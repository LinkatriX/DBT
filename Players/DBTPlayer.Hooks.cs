using System.Collections.Generic;
using DBT.Commons.Players;
using DBT.Effects;
using DBT.HairStyles;
using DBT.Transformations;
using DBT.Wasteland;
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

        /*#region Sync Triggers
         public bool? syncTriggerSetMouseLeft;
         public bool? syncTriggerSetMouseRight;
         public bool? syncTriggerSetLeft;
         public bool? syncTriggerSetRight;
         public bool? syncTriggerSetUp;
         public bool? syncTriggerSetDown;
         #endregion*/

        /*public override void ProcessTriggers(TriggersSet triggersSet)
        {
            UpdateSynchronizedControls(triggersSet);

            //SyncTriggerSet();

            if (flightToggleKey.JustPressed)
            {
                if (FlightUnlocked)
                {
                    isFlying = !isFlying;
                    if (!isFlying)
                    {
                        FlightSystem.AddKatchinFeetBuff(player);
                    }
                }
            }

            //_mProgressionSystem.Update(player);
        }*/

        /*public void UpdateSynchronizedControls(TriggersSet triggerSet)
        {
            // this might look weird, but terraria seemed to treat these getters as changing the collection, resulting in some really strange errors/behaviors.
            // change these to normal ass setters at your own peril.
            if (triggerSet.Left)
                isLeftHeld = true;
            else
                isLeftHeld = false;

            if (triggerSet.Right)
                isRightHeld = true;
            else
                isRightHeld = false;

            if (triggerSet.Up)
                isUpHeld = true;
            else
                isUpHeld = false;

            if (triggerSet.Down)
                isDownHeld = true;
            else
                isDownHeld = false;

            if (triggerSet.MouseRight)
                isMouseRightHeld = true;
            else
                isMouseRightHeld = false;

            if (triggerSet.MouseLeft)
                isMouseLeftHeld = true;
            else
                isMouseLeftHeld = false;
        }*/

        //Gonna have to look into what the current network files have that replaced the old mod -Skipping
        /*public void SyncTriggerSet()
        {
            // if we're not in network mode, do nothing.            
            if (Main.netMode != NetmodeID.MultiplayerClient)
                return;

            // if this method is firing on a player who isn't me, abort. 
            // spammy af
            if (Main.myPlayer != player.whoAmI)
                return;

            if (syncTriggerSetLeft != isLeftHeld)
            {
                NetworkHelper.playerSync.SendChangedTriggerLeft(256, player.whoAmI, player.whoAmI, isLeftHeld);
                syncTriggerSetLeft = isLeftHeld;
            }
            if (syncTriggerSetRight != isRightHeld)
            {
                NetworkHelper.playerSync.SendChangedTriggerRight(256, player.whoAmI, player.whoAmI, isRightHeld);
                syncTriggerSetRight = isRightHeld;
            }
            if (syncTriggerSetUp != isUpHeld)
            {
                NetworkHelper.playerSync.SendChangedTriggerUp(256, player.whoAmI, player.whoAmI, isUpHeld);
                syncTriggerSetUp = isUpHeld;
            }
            if (syncTriggerSetDown != isDownHeld)
            {
                NetworkHelper.playerSync.SendChangedTriggerDown(256, player.whoAmI, player.whoAmI, isDownHeld);
                syncTriggerSetDown = isDownHeld;
            }

            if (syncTriggerSetMouseRight != isMouseRightHeld)
            {
                NetworkHelper.playerSync.SendChangedTriggerMouseRight(256, player.whoAmI, player.whoAmI, isMouseRightHeld);
                syncTriggerSetMouseRight = isMouseRightHeld;
            }

            if (syncTriggerSetMouseLeft != isMouseLeftHeld)
            {
                NetworkHelper.playerSync.SendChangedTriggerMouseLeft(256, player.whoAmI, player.whoAmI, isMouseLeftHeld);
                syncTriggerSetMouseLeft = isMouseLeftHeld;
            }
        }*/

        public override void ResetEffects()
        {
            HealthDrainMultiplier = 0;
            _aliveBosses = null;

            ResetKiEffects();
            ResetGuardianEffects();
            ResetSkillEffects();
            ResetOverloadEffects();

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

            // neuters flight if the player gets immobilized. Note the lack of Katchin Feet buff.
            if (IsPlayerImmobilized() && IsFlying)
            {
                IsFlying = false;
            }

            List<IHandleOnPlayerPostUpdate> items = player.GetItemsByType<IHandleOnPlayerPostUpdate>();

            for (int i = 0; i < items.Count; i++)
                items[i].OnPlayerPostUpdate(this);

            if (Worlds.DBTWorld.friezaShipTriggered && !NPC.AnyNPCs(mod.NPCType("FriezaShip")))
                CheckFriezaShipSpawn();

            HandleMouseOctantAndSyncTracking();
            //HandleChargeEffects();

            // flight system moved to PostUpdate so that it can benefit from not being client sided!
            FlightSystem.Update(player);
        }

        public override void PostUpdateRunSpeeds()
        {
            if (IsCharging)
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

            IsCharging = false;
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            PlayerTransformation transformation = GetTransformation();

            if (transformation == null)
            {
                if (originalEyeColor.HasValue && player.eyeColor != originalEyeColor.Value)
                    player.eyeColor = originalEyeColor.Value;

                return;
            }


            HandleAuraDrawLayers(layers);
            HandleHairDrawLayers(layers);

            // handle dragon radar drawing
            if (IsHoldingDragonRadarMk1 || IsHoldingDragonRadarMk2 || IsHoldingDragonRadarMk3)
            {
                DrawDragonRadar.dragonRadarEffects.visible = true;
                layers.Add(DrawDragonRadar.dragonRadarEffects);
            }

            if (transformation.Definition.Appearance.EyeColor != null)
                ChangeEyeColor(transformation.Definition.Appearance.EyeColor.Value);
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
    }
}