using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DBT.NPCs.Spirits;

namespace DBT.Projectiles
{
    public class RitualGhostsSpawn : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Test");
        }

        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.light = 0f;
            projectile.knockBack = 0f;
            projectile.alpha = 0;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.netUpdate = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 1;
            projectile.aiStyle = 1;
            aiType = 14;
        }

        public override void AI()
        {
            Player player = Main.LocalPlayer;
            NPC.NewNPC((int)player.position.X - 40, (int)player.position.X - 50, ModContent.NPCType<BardockGhostBase>());
        }
    }
}