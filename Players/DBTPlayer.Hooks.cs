using System.Collections.Generic;
using DBT.Commons.Players;
using DBT.Effects;
using DBT.HairStyles;
using DBT.NPCs.Bosses.FriezaShip;
using DBT.Traits;
using DBT.Transformations;
using DBT.Wasteland;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Extensions;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private const float CHARGING_MOVE_SPEED_MULTIPLIER = 0.5f;

        public bool zoneWasteland = false;

        public static readonly PlayerLayer tailLayer = new DrawTailEffects(0);
        public static readonly PlayerLayer furLayer = new DrawBodyEffects();
        public static readonly PlayerLayer customBodySkin = new CustomBodySkinLayer();

        public override void Initialize()
        {
            Aura = null;
            InitializeTransformations();
            InitializeSkills();

            if (!PlayerInitialized)
            {
                ChosenHairStyle = HairStyleManager.Instance.NoChoice;

                Ki = 0;
                BaseMaxKi = 500;
            }

            PlayerInitialized = true;
        }

        public override void ResetEffects()
        {
            HealthDrainMultiplier = 0;
            _aliveBosses = null;

            ResetKiEffects();
            ResetGuardianEffects();
            ResetSkillEffects();
            ResetOverloadEffects();
            ResetFlightEffects();

            IsHoldingDragonRadarMk1 = false;
            IsHoldingDragonRadarMk2 = false;
            IsHoldingDragonRadarMk3 = false;
        }


        #region Pre Update

        public override void PreUpdate()
        {
            PreUpdateKi();
            PreUpdateOverload();
        }

        public override void PreUpdateMovement()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                PreUpdateMovementHandleAura();
                PreUpdateMovementHandleHair();
            }
        }

        #endregion


        #region Post Update

        public override void PostUpdate()
        {
            FirstTransformation = GetTransformation();

            PostUpdateKi();
            PostUpdateOverload();
            PostUpdateHandleTransformations();
            UpdateNPCs();
            PostUpdateTiles();
            PostUpdateDragonBalls();

            // neuters flight if the player gets immobilized. Note the lack of Katchin Feet buff.
            if (IsPlayerImmobilized() && Flying)
            {
                Flying = false;
            }

            List<IHandleOnPlayerPostUpdate> items = player.GetItemsByType<IHandleOnPlayerPostUpdate>();

            for (int i = 0; i < items.Count; i++)
                items[i].OnPlayerPostUpdate(this);

            if (Worlds.DBTWorld.friezaShipTriggered && !NPC.AnyNPCs(ModContent.NPCType<FriezaShip>()))
                CheckFriezaShipSpawn();

            HandleMouseOctantAndSyncTracking();
            //HandleChargeEffects();

            // Flight system moved to PostUpdate so that it can benefit from not being client sided!
            Flight.Update(this);

            TailFrameTimer++;
            if (TailFrameTimer > 112)
                TailFrameTimer = 0;
            /*if (IsTransformationAnimationPlaying)
            {
                player.velocity = new Vector2(0, player.velocity.Y);

                TransformationFrameTimer++;
            }
            else
            {
                TransformationFrameTimer = 0;
            }*/
        }

        public override void PostUpdateRunSpeeds()
        {
            if (Charging)
            {
                player.moveSpeed *= CHARGING_MOVE_SPEED_MULTIPLIER;
                player.maxRunSpeed *= CHARGING_MOVE_SPEED_MULTIPLIER;
                player.runAcceleration *= CHARGING_MOVE_SPEED_MULTIPLIER;
            }
        }

        #endregion


        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            TransformationDefinitionManager.Instance.ForAllItems(t => t.OnPreAcquirePlayerDied(this, damage, pvp, damageSource));

            ForAllActiveTransformations(p => p.OnActivePlayerDied(this, damage, pvp, damageSource));
            ClearTransformations();

            Charging = false;
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            PlayerTransformation transformation = GetTransformation();

            HandleAuraDrawLayers(layers);

            if (transformation == null)
            {
                if (originalEyeColor.HasValue && player.eyeColor != originalEyeColor.Value)
                    player.eyeColor = originalEyeColor.Value;

                //return;
            }
            
            HandleHairDrawLayers(layers);

            /*if (Trait == TraitManager.Instance.Primal)
            {
                
            }*/
            tailLayer.visible = true;
            layers.Insert(layers.FindIndex(l => l.Name == "MiscEffectsBack"), tailLayer);

            furLayer.visible = true;

            PlayerLayer skinLayer = layers.Find(l => l.Name.Equals(nameof(PlayerLayer.Skin)));
            int skinIndex = layers.IndexOf(skinLayer);


            for (int i = 0; i < ActiveTransformations.Count; i++)
                if (ActiveTransformations[i].Appearance.ShouldHideNormalSkin)
                {
                    layers.RemoveAt(skinIndex);
                    layers.Insert(skinIndex, customBodySkin);

                    break;
                }


            layers.Insert(skinIndex + 1, furLayer);

            // handle dragon radar drawing
            if (IsHoldingDragonRadarMk1 || IsHoldingDragonRadarMk2 || IsHoldingDragonRadarMk3)
            {
                DrawDragonRadar.dragonRadarEffects.visible = true;
                layers.Add(DrawDragonRadar.dragonRadarEffects);
            }

            if (transformation != null)
            {
                if (transformation.Definition.Appearance.EyeColor.HasValue)
                    ChangeEyeColor(transformation.Definition.Appearance.EyeColor.Value);
            }
            // handle transformation animations
            /*transformationEffects.visible = true;
            layers.Add(transformationEffects);*/
        }

        public override void UpdateBiomes()
        {
            zoneWasteland = (WastelandWorld.wastelandTiles > 100);
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            List<IHandleOnPlayerHitNPC> items = player.GetItemsByType<IHandleOnPlayerHitNPC>(armor: true, accessories: true);

            for (int i = 0; i < items.Count; i++)
                items[i].OnPlayerHitNPC(item, target, ref damage, ref knockback, ref crit);

            base.OnHitNPC(item, target, damage, knockback, crit);
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            List<IHandleOnPlayerPreHurt> items = player.GetItemsByType<IHandleOnPlayerPreHurt>();

            for (int i = 0; i < items.Count; i++)
                if (!items[i].OnPlayerPreHurt(this, pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource))
                    return false;

            return true;
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            // bool baseResult = base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
            List<IHandleOnPlayerPreKill> items = new List<IHandleOnPlayerPreKill>();

            for (int i = 0; i < items.Count; i++)
                if (!items[i].OnPlayerPreKill(this, ref damage, ref hitDirection, ref pvp, ref playSound, ref genGore, ref damageSource))
                    return false;

            return true;
        }

        public int TailFrameTimer { get; set; }
    }
}