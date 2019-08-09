using System.Collections.Generic;
using DBT.Commons.Players;
using DBT.Extensions;
using DBT.HairStyles;
using DBT.Transformations;
using DBT.Wasteland;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

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

            if (!PlayerInitialized)
            {
                ChosenHairStyle = HairStyleManager.Instance.NoChoice;

                Ki = 0;
                BaseMaxKi = 1000;
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

            List<IHandleOnPlayerPostUpdate> items = player.GetItemsByType<IHandleOnPlayerPostUpdate>();

            for (int i = 0; i < items.Count; i++)
                items[i].OnPlayerPostUpdate(this);

            if (DBTWorld.friezaShipTriggered && !NPC.AnyNPCs(mod.NPCType("FriezaShip")))
                CheckFriezaShipSpawn();
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
            HandleAuraDrawLayers(layers);
            HandleHairDrawLayers(layers);
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