using DBT.Helpers;
using DBT.Items.DragonBalls;
using DBT.NPCs.Misc;
using DBT.Projectiles.Animations;
using DBT.UserInterfaces.WishMenu;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Extensions;

namespace DBT.Players
{
    public partial class DBTPlayer
    {
        int soundTimer = 0;
        public void PostUpdateDragonBalls()
        {
            if (WishActive)
                DBSummonEffects();
            soundTimer++;
            if (CarryingAllDragonBalls(player) && !WishActive)
                if (soundTimer > 300)
                {
                    SoundHelper.PlayCustomSound("Sounds/DBReady", player, 0.5f);
                    soundTimer = 0;
                }
                    
        }

        public void DestroyOneOfEachDragonBall(Player player)
        {
            List<int> dragonBallTypeAlreadyRemoved = new List<int>();
            foreach (var item in player.inventory)
            {
                if (item == null)
                    continue;
                if (item.modItem == null)
                    continue;
                if (item.modItem is DragonBall)
                {
                    // only remove one of each type of dragon ball. If the player has extras, leave them. Lucky them.
                    if (dragonBallTypeAlreadyRemoved.Contains(item.type))
                        continue;
                    dragonBallTypeAlreadyRemoved.Add(item.type);
                    item.TurnToAir();
                }
            }
        }

        public void DBSummonEffects()
        {
            if (NPC.CountNPCS(ModContent.NPCType<ShenronNPC>()) == 0)
            {
                int dust = Dust.NewDust(player.Center, player.width, player.height / 3, 133, 0, -25f);
                dust = Dust.NewDust(player.Center, player.width, player.height / 3, 133, 0, -25f);
                Main.dust[dust].noGravity = true;
                if (DBTMod.IsTickRateElapsed(80))
                {
                    Main.dust[dust].velocity.Y = -25f;
                    Main.dust[dust].scale = 1f;
                }
                if (DBTMod.IsTickRateElapsed(160))
                {
                    Main.dust[dust].velocity.X = Main.rand.Next(-30, 30);
                    Main.dust[dust].velocity.Y = 0;
                    Main.dust[dust].scale = 1f;
                }
                if (DBTMod.IsTickRateElapsed(80))
                {
                    Projectile.NewProjectile(player.Center + new Vector2(Main.rand.Next(-700, 700), -50), Vector2.Zero, ModContent.ProjectileType<YellowLightning>(), 0, 0);
                    SoundHelper.PlayCustomSound("Sounds/Lightning", player, 0.5f);
                }
                if (DBTMod.IsTickRateElapsed(500))
                {
                    Main.time = 72000;
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y, ModContent.NPCType<ShenronNPC>());
                    NetMessage.SendData(MessageID.SyncNPC);
                }
            }
            else
            {
                if (DBTMod.IsTickRateElapsed(500))
                {
                    WishMenu.menuVisible = true;
                }
            }
        }

        public void DoRitual()
        {

        }

        public bool CarryingAllDragonBalls(Player player)
        {
            List<DragonBall> dragonBalls = player.GetItemsByType<DragonBall>(inventory: true);
            List<DragonBallStarCount> dragonBallStars = new List<DragonBallStarCount>(7);

            for (int i = 0; i < dragonBalls.Count; i++)
                if (!dragonBallStars.Contains(dragonBalls[i].StarCount))
                    dragonBallStars.Add(dragonBalls[i].StarCount);

            return dragonBallStars.Count == Enum.GetNames(typeof(DragonBallStarCount)).Length;
        }

        public const int POWER_WISH_MAXIMUM = 5;
        public bool WishActive { get; set; }        
        public int PowerWishesLeft { get; set; } = 5;
        public int ImmortalityWishesLeft { get; set; } = 1;
        public int SkillWishesLeft { get; set; } = 3;
        public int AwakeningWishesLeft { get; set; } = 5;
        public int ImmortalityRevivesLeft { get; set; }
        public bool IsHoldingDragonRadarMk1 { get; set; }
        public bool IsHoldingDragonRadarMk2 { get; set; }
        public bool IsHoldingDragonRadarMk3 { get; set; }
        public bool FirstDragonBallPickup { get; set; }
    }
}
