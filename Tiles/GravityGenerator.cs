using DBT.Helpers;
using DBT.Items.Materials.Metals;
using DBT.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace DBT.Tiles
{
    public class GravityGenerator : ModTile
    {
        internal bool _activated = false;
        internal bool creatingDust = false;
        internal float windupTimer = 0;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileTable[Type] = false;
            Main.tileLavaDeath[Type] = false;
            Main.tileNoFail[Type] = true;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.Width = 11;
            TileObjectData.newTile.Height = 12;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 18 };
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.DrawYOffset = 8;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Gravity Generator");
            AddMapEntry(new Color(223, 245, 255), name);
            animationFrameHeight = 216;
            minPick = 10000;
            TileObjectData.addTile(Type);
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (_activated)
                frame = 2;
            else if (DBTWorld.repairedGravModule)
                frame = 1;
            else
                frame = 0;
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (_activated)
            {
                if (windupTimer >= 100)
                    windupTimer = 100;
                
                windupTimer += 0.003f;

                Dust dust;
                if (Main.rand.NextFloat() < windupTimer / 150f)
                {
                    
                    dust = Dust.NewDustDirect(BaseUtility.TileToPos(new Vector2(i, j)) - new Vector2(1000, 200), 2500, 1000, 264, 0f, -30f, 0, new Color(255, 255, 255), 1f);
                    dust.noGravity = true;
                    dust.noLight = true;
                    dust.fadeIn = 0.75f;
                    dust.velocity.X = 0f;
                }
                
                Lighting.AddLight(BaseUtility.TileToPos(new Vector2(i, j)), new Vector3(windupTimer * 10f, 0, 0));
            }
            return base.PreDraw(i, j, spriteBatch);
        }
        
        public override void RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            if (!DBTWorld.repairedGravModule)
            {
                if (player.HasItem(ModContent.ItemType<RefinedMetal>()))
                {
                    DBTWorld.repairedGravModule = true;
                    if (Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.WorldData);
                    Main.NewText("Gravity Generator Repaired!");
                    SoundHelper.PlayCustomSound("Sounds/GChamberRepair", player, 0.7f);
                    
                }
                else
                {
                    Main.NewText("You do not have enough refined metal to fix this machine!");
                }
            }
            else if (DBTWorld.repairedGravModule)
            {
                if (!_activated)
                {
                    SoundHelper.PlayCustomSound("Sounds/GChamberStart", player, 0.6f);
                    _activated = true;
                    creatingDust = true;
                    for (int h = 0; h < 6; h++)
                    {
                        CircularDust(i, j, 20, 20, 100, 1);
                    }
                }
                    
                else if (_activated)
                {
                    _activated = false;
                    windupTimer = 0;
                }
                    
            }
        }

        public void CircularDust(int i, int j, int quantity, short dustID, float radius, float scale)
        {
            Tile tile = Main.tile[i, j];
            for (int l = 0; l < quantity; l++)
            {
                Vector2 pos = new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) + BaseUtility.TileToPos(new Vector2(i, j));
                float angle = Main.rand.NextFloat(-(float)Math.PI, (float)Math.PI);
                Dust dust = Dust.NewDustPerfect(pos, dustID,
                    new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * radius, 255, default(Color),
                    scale);
                dust.noGravity = true;
            }
            creatingDust = false;
        }
    }
}
