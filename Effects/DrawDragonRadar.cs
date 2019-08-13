using DBT.Players;
using DBT.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DBT.Effects
{
    public sealed class DrawDragonRadar
    {
        public static readonly PlayerLayer dragonRadarEffects = new PlayerLayer(DBTMod.Instance.Name, "DragonRadarEffects", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
        {
            // ragon radar effects only show up for the player holding it.
            if (drawInfo.drawPlayer.whoAmI != Main.myPlayer)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            DBTPlayer modPlayer = drawPlayer.GetModPlayer<DBTPlayer>();
            Mod mod = DBTMod.Instance;
            if (drawInfo.shadow != 0f)
            {
                return;
            }

            Point closestLocation = Point.Zero;
            float closestDistance = float.MaxValue;
            for (int i = 1; i <= 7; i++)
            {
                var location = DBTMod.Instance.GetModWorld<DBTWorld>().GetCachedDragonBallLocation(5);
                if (location.Equals(Point.Zero))
                    continue;
                // skip this dragon ball if the player is holding a copy
                if (Main.LocalPlayer.inventory.IsDragonBallPresent(i))
                    continue;
                var coordVector = location.ToVector2() * 16f;
                var distance = Vector2.Distance(coordVector, drawPlayer.Center + Vector2.UnitY * -120f);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestLocation = location;
                }
            }

            if (closestLocation.Equals(Point.Zero))
            {
                // not a valid location, abort.
                return;
            }

            Vector2 radarAngleVector = Vector2.Normalize((drawPlayer.Center + Vector2.UnitY * -120f) - (closestLocation.ToVector2() * 16f));
            float radarAngle = radarAngleVector.ToRotation();

            // player is too close to the dragon ball.
            if (closestDistance < (modPlayer.isHoldingDragonRadarMk1 ? 1280f : (modPlayer.isHoldingDragonRadarMk2 ? 640f : 320f)))
            {
                radarAngle += (float)(DBTMod.GetTicks() % 59) * 6f;
            }
            radarAngle += MathHelper.ToRadians(radarAngle) - drawPlayer.fullRotation;
            var yOffset = -120;
            Main.playerDrawData.Add(DragonRadarDrawData(drawInfo, "Items/DragonBalls/DragonRadarPointer", yOffset, radarAngle - 1.57f, closestDistance, closestLocation.ToVector2() * 16f));

        });

        public static DrawData DragonRadarDrawData(PlayerDrawInfo drawInfo, string dragonRadarSprite, int yOffset, float angleInRadians, float distance, Vector2 location)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = DBTMod.Instance;
            DBTPlayer modPlayer = drawPlayer.GetModPlayer<DBTPlayer>(mod);
            float radarArrowScale = (modPlayer.isHoldingDragonRadarMk1 ? 1f : (modPlayer.isHoldingDragonRadarMk2 ? 1.25f : 1.5f));
            Texture2D texture = mod.GetTexture(dragonRadarSprite);
            int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
            int drawY = (int)(drawInfo.position.Y + yOffset + drawPlayer.height / 0.6f - Main.screenPosition.Y);
            return new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, angleInRadians, new Vector2(texture.Width / 2f, texture.Height / 2f), radarArrowScale, SpriteEffects.None, 0);
        }
    }
}
