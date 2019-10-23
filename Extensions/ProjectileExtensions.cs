using DBT.Players;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DBT.Extensions
{
    public static class ProjectileExtensions
    {
        public static void StartKillRoutine(this Projectile projectile)
        {
            if (projectile == null)
                return;

            if (projectile.localAI[0] == 0)
                projectile.localAI[0] = 1;
        }

        public static void ApplyChannelingSlowdown(this Player player)
        {
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            if (modPlayer.Flying)
            {
                float chargeMoveSpeedBonus = modPlayer.SkillChargeMoveSpeedModifier / 10f;
                float yVelocity = -(player.gravity + 0.001f);
                if (modPlayer.DownHeld || modPlayer.UpHeld)
                {
                    yVelocity = player.velocity.Y / (1.2f - chargeMoveSpeedBonus);
                }
                else
                {
                    yVelocity = Math.Min(-0.4f, player.velocity.Y / (1.2f - chargeMoveSpeedBonus));
                }
                player.velocity = new Vector2(player.velocity.X / (1.2f - chargeMoveSpeedBonus), yVelocity);
            }
            else
            {
                float chargeMoveSpeedBonus = modPlayer.SkillChargeMoveSpeedModifier / 10f;
                // don't neuter falling - keep the positive Y velocity if it's greater - if the player is jumping, this reduces their height. if falling, falling is always greater.                        
                player.velocity = new Vector2(player.velocity.X / (1.2f - chargeMoveSpeedBonus), Math.Max(player.velocity.Y, player.velocity.Y / (1.2f - chargeMoveSpeedBonus)));
            }
        }
    }
}
