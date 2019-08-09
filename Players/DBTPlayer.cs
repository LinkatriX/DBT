using System;
using DBT.Transformations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Players
{
    public sealed partial class DBTPlayer : ModPlayer
    {
        public void OnKilledNPC(NPC npc)
        {
            TransformationDefinitionManager.Instance.ForAllItems(t => t.OnPreAcquirePlayerKilledNPC(this, npc));
            ForAllActiveTransformations(t => t.OnActivePlayerKilledNPC(this, npc));
        }

        public void ApplySkillChargeSlowdown()
        {
            float chargeMoveSpeedBonus = 0;

            if (Flying)
            {
                chargeMoveSpeedBonus = SkillChargeMoveSpeedModifier / 10f;
                float yVelocity = player.gravity + 0.001f;

                if (DownHeld || UpHeld)
                    yVelocity = player.velocity.Y / (1.2f - chargeMoveSpeedBonus);
                else
                    yVelocity = Math.Min(-0.4f, player.velocity.Y / (1.2f - chargeMoveSpeedBonus));
            }
            else
            {
                chargeMoveSpeedBonus = SkillChargeMoveSpeedModifier / 10f;

                player.velocity = new Vector2(player.velocity.X / (1.2f - chargeMoveSpeedBonus), Math.Max(player.velocity.Y, player.velocity.Y / (1.2f - chargeMoveSpeedBonus)));
            }
        }

        public override void OnEnterWorld(Player player)
        {
            HandleTransformationsOnEnterWorld(player);
        }

        public bool PlayerInitialized { get; private set; }

        public float HealthDrainMultiplier { get; set; }
    }
}
