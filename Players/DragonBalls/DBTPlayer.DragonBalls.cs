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
        int dustAlive = 0;
        public void DBSummonEffects()
        {
            if (NPC.CountNPCS(ModContent.NPCType<ShenronNPC>()) == 0)
            {
                Dust dust = null;
                dustAlive++;
                for (int i = 0; i < 2; i++)
                {
                    dust = Dust.NewDustPerfect(player.Center + new Vector2(0, -80), 133, new Vector2(0f, -20f));
                    dust.noGravity = true;
                    dust.velocity *= 0.99f;
                }
                if (dust.position.Y >= player.Center.Y + 100 - dustAlive / 5)
                {
                    dust.velocity.Y = 0f;
                    dust.scale = 0f;
                }
                if (dustAlive >= 60)
                {
                    CircularDust(10, new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) + player.Center + new Vector2(0, -340), 133, 10 + dustAlive / 1.6f, 0.9f);
                }
                if (DBTMod.IsTickRateElapsed(50))
                {
                    Projectile.NewProjectile(player.Center + new Vector2(Main.rand.Next(-700, 700), -50), Vector2.Zero, ModContent.ProjectileType<YellowLightning>(), 0, 0);
                    SoundHelper.PlayCustomSound("Sounds/Lightning", player, 0.5f);
                }
                if (dustAlive == 500)
                {
                    Main.time = 72000;
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y, ModContent.NPCType<ShenronNPC>());
                    NetMessage.SendData(MessageID.SyncNPC);
                }
            }
            else
            {
                if (DBTMod.IsTickRateElapsed(300))
                {
                    dustAlive = 0;
                    WishMenu.menuVisible = true;
                }
            }
        }

        public void DoRitual()
        {

        }

        public void CircularDust(int quantity, Vector2 pos, short dustID, float radius, float scale)
        {
            for (int i = 0; i < quantity; i++)
            {
                float angle = Main.rand.NextFloat(-(float)Math.PI, (float)Math.PI);
                Dust dust = Dust.NewDustPerfect(pos + new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle)) * radius, dustID, Vector2.Zero, 255, default(Color),
                    scale);
                dust.noGravity = true;
                dust.velocity *= 0.98f;
            }
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
