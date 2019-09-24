using System;
using DBT.Extensions;
using DBT.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Auras
{
    public sealed class AuraPlayerLayer : PlayerLayer
    {
        public AuraPlayerLayer(int index) : base(DBTMod.Instance.Name, "AuraLayer" + index, null, DrawLayer)
        {
        }

        private static void DrawLayer(PlayerDrawInfo drawInfo)
        {
            if (Main.netMode == NetmodeID.Server)
                return;

            DBTPlayer dbtPlayer = drawInfo.drawPlayer.GetModPlayer<DBTPlayer>();

            AuraAppearance aura = dbtPlayer.GetAura();

            if (aura == null)
                return;

            int auraHeight = aura.Information.GetHeight(dbtPlayer);

            Texture2D auraTexture = aura.Information.GetTexture(dbtPlayer);
            Rectangle auraRectangle = new Rectangle(0, auraHeight * dbtPlayer.AuraFrameIndex, auraTexture.Width, auraHeight);

            float scale = aura.Information.GetAuraScale(dbtPlayer);
            Tuple<float, Vector2> rotationAndPosition = aura.Information.GetRotationAndPosition(dbtPlayer);

            SamplerState samplerState = dbtPlayer.GetPlayerSamplerState();
            aura.Information.BlendState.SetSpriteBatchForPlayerLayerCustomDraw(samplerState);

            Main.spriteBatch.Draw(auraTexture, rotationAndPosition.Item2 - Main.screenPosition, auraRectangle, Color.White, rotationAndPosition.Item1,
                new Vector2(aura.Information.GetWidth(dbtPlayer), aura.Information.GetHeight(dbtPlayer)) * 0.5f, scale, SpriteEffects.None, 0f);

            samplerState.ResetSpriteBatchForPlayerDrawLayers();
        }
    }
}
