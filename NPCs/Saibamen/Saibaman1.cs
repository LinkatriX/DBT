using DBT.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.NPCs.Saibamen
{
    public class Saibaman1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Saibaman");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 36;
            npc.damage = 54;
            npc.defense = 4;
            npc.lifeMax = 50;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 60f;
            npc.knockBackResist = 0.3f;
            npc.aiStyle = 3;
            aiType = NPCID.VortexSoldier;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.GetModPlayer<DBTPlayer>().zoneWasteland ? 1f : 0f;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            npc.TargetClosest(true);

            if (Vector2.Distance(new Vector2(player.position.X, 0), new Vector2(npc.position.X, 0)) <= 120)
            {
                soundTimer++;
                if (soundTimer > (180 + Main.rand.Next(60, 120)))
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, GeGeGe()).WithVolume(0.6f));
                    soundTimer = 0;
                }
            }

            if (Vector2.Distance(new Vector2(player.position.X, 0), new Vector2(npc.position.X, 0)) <= 60)
            {
                jumpTimer++;
                if (jumpTimer == 1 && npc.velocity.Y == 0)
                {
                    npc.velocity = new Vector2(3f * npc.direction, -8f);
                }
                if (jumpTimer > 10)
                {
                    //npc.velocity.Y = 0;
                    if (jumpTimer > 20)
                    {
                        npc.velocity.X = 0;
                        jumpTimer = 0;
                    }
                }
            }
            if (grabbed)
            {
                npc.position = Main.player[npc.target].position;
                explodeTimer++;
                if (explodeTimer > 20)
                {
                    Explode();
                }
            }
            base.AI();
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            grabbed = true;
            if (target.statLife == 0)
            {
                Player player = Main.player[npc.target];
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + "has been yamcha'd."), 54, npc.spriteDirection);
            }
        }

        int frame = 0;
        public override void FindFrame(int frameHeight)
        {
            if (!grabbed)
            {
                npc.frameCounter += 1;
            }
            else
            {
                npc.frameCounter = 0;
            }
            if (npc.frameCounter > 4)
            {
                frame++;
                npc.frameCounter = 0;
            }
            if (frame > 2 && !grabbed)
            {
                frame = 0;
            }
            if (grabbed)
            {
                frame = 3;
            }
            npc.spriteDirection = -npc.direction;
            npc.frame.Y = frameHeight * frame;
        }

        public void Explode()
        {
            for (int num619 = 0; num619 < 3; num619++)
            {
                float scaleFactor9 = 3f;
                if (num619 == 1)
                {
                    scaleFactor9 = 3f;
                }
                npc.width = 60;
                npc.height = 60;
                npc.damage = 140;
                int num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore97 = Main.gore[num620];
                gore97.velocity.X = gore97.velocity.X + 1f;
                Gore gore98 = Main.gore[num620];
                gore98.velocity.Y = gore98.velocity.Y + 1f;
                num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore99 = Main.gore[num620];
                gore99.velocity.X = gore99.velocity.X - 1f;
                Gore gore100 = Main.gore[num620];
                gore100.velocity.Y = gore100.velocity.Y + 1f;
                num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore101 = Main.gore[num620];
                gore101.velocity.X = gore101.velocity.X + 1f;
                Gore gore102 = Main.gore[num620];
                gore102.velocity.Y = gore102.velocity.Y - 1f;
                num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore103 = Main.gore[num620];
                gore103.velocity.X = gore103.velocity.X - 1f;
                Gore gore104 = Main.gore[num620];
                gore104.velocity.Y = gore104.velocity.Y - 1f;
                npc.active = false;
            }
        }
        public string GeGeGe()
        {
            switch (Main.rand.Next(0, 2))
            {
                case 0:
                    return "Sounds/Saibamen/Saibamen1";
                case 1:
                    return "Sounds/Saibamen/Saibamen2";
                case 2:
                    return "Sounds/Saibamen/Saibamen3";
                default:
                    return "";

            }
        }

        public bool assignedTexture = false;
        public int jumpTimer = 0;
        public int explodeTimer = 0;
        public int soundTimer = 0;
        public bool grabbed = false;
    }
}

