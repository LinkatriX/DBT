﻿using System;
using System.Collections.Generic;
using System.Linq;
using DBT.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace DBT.UserInterfaces
{
    // TODO Make this use classes/structs.
    public class EnergyResourceBar : UIElement
    {
        public UIText label;
        public Rectangle dragRectangle;

        public int frameTimer;

        public static readonly List<float> _cleanAverageEnergy = new List<float>();

        public EnergyResourceBar(int cWidth, int cHeight, int segments)
        {
            CWidth = cWidth;
            CHeight = cHeight;

            Segments = segments;
        }


        public override void OnInitialize()
        {
            Width.Set(CWidth, 0f);
            Height.Set(CHeight, 0f);

            label = new UIText("0/0");

            label.Width.Set(CWidth, 0f);
            label.Height.Set(CHeight, 0f);

            label.Left.Set(14f, 0f);
            label.Top.Set(20f, 0f);

            Append(label);
            dragRectangle = new Rectangle(6, 6, (int) CWidth - 2, (int) CHeight - 6);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            float quotient = Utils.Clamp((float) Math.Floor(_cleanAverageEnergy.Sum() / 15f) / dbtPlayer.MaxKi, 0, 1);

            Rectangle hitBox = GetInnerDimensions().ToRectangle();
            hitBox.X += dragRectangle.X;
            hitBox.Y += dragRectangle.Y;

            hitBox.Width = dragRectangle.Width;
            hitBox.Height = dragRectangle.Height;

            frameTimer++;

            if (frameTimer >= 20)
                frameTimer = 0;

            int frameHeight = 0;
            int frame = frameTimer / 5;

            Vector2 textureOffset = Vector2.Zero;

            // TODO !!IMPORTANT!! CHANGE THIS TO USE OBJECTS 100%.
            Texture = DBTMod.Instance.GetTexture("UserInterfaces/KiBar/DefaultKiBarFrame");

            frameHeight = Texture.Height / 4;
            textureOffset = new Vector2(16, 8);

            Position = new Vector2(hitBox.X - 6, hitBox.Y - 6);

            Rectangle sourceRectangle = new Rectangle(0, frameHeight * frame, Texture.Width, frameHeight);
            spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White);

            Texture2D barSegmentTexture = DBTMod.Instance.GetTexture("UserInterfaces/KiBar/DefaultKiBar");

            int segmentsCount = (int) Math.Ceiling(Segments * quotient);

            for (int i = 0; i < segmentsCount; i++)
            {
                Vector2 segmentOffsetX = new Vector2(i * 12, 0);
                Vector2 segmentPosition = Position + textureOffset + segmentOffsetX;

                if (i == segmentsCount - 1)
                {
                    float segmentValue = 1f / Segments;
                    float segmentRemainder = quotient % segmentValue;
                    float segmentQuotient = segmentRemainder / segmentValue;

                    if (segmentQuotient == 0f)
                        segmentQuotient = 1f;

                    spriteBatch.Draw(barSegmentTexture, segmentPosition, new Rectangle(0, 0, (int) Math.Ceiling(barSegmentTexture.Width * segmentQuotient), barSegmentTexture.Height), Color.White);
                }
                else
                    spriteBatch.Draw(barSegmentTexture, segmentPosition, new Rectangle(0, 0, barSegmentTexture.Width, barSegmentTexture.Height), Color.White);
            }
        }
        internal static Random random = new Random();
        public static char GetLetter()
        {
            int num = random.Next(0, 26);
            char let = (char)('a' + num);
            return let;
        }

        // TODO Rewrite this to use objects.
        public override void Update(GameTime gameTime)
        {
            
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            _cleanAverageEnergy.Add(dbtPlayer.Ki);

            if (_cleanAverageEnergy.Count > 15)
                _cleanAverageEnergy.RemoveRange(0, _cleanAverageEnergy.Count - 15);

            int averageKi = (int) Math.Floor(_cleanAverageEnergy.Sum() / 15f);
            if (!dbtPlayer.IsOverloading)
                label.SetText("Ki: " + averageKi + " / " + dbtPlayer.MaxKi);
            else
                label.SetText("Ki: " + GetLetter() + GetLetter()+ GetLetter() + GetLetter() + " / " + GetLetter() + GetLetter() + GetLetter() + GetLetter());


            base.Update(gameTime);
        }


        public float CWidth { get; }

        public float CHeight { get; }


        public int Segments { get; }


        public Vector2 Position { get; set; }

        public Texture2D Texture { get; set; }
    }
    public class OverloadResourceBar : UIElement //Totally not just copying the whole thing and changing a few variables
    {
        public UIText label;
        public Rectangle dragRectangle;

        public int frameTimer;

        public static readonly List<float> _cleanAverageEnergy = new List<float>();

        public OverloadResourceBar(int cWidth, int cHeight, int segments)
        {
            CWidth = cWidth;
            CHeight = cHeight;

            Segments = segments;
        }
        public override void OnInitialize()
        {
            Width.Set(CWidth, 0f);
            Height.Set(CHeight, 0f);

            label = new UIText("0/0");

            label.Width.Set(CWidth, 0f);
            label.Height.Set(CHeight, 0f);

            label.Left.Set(14f, 0f);
            label.Top.Set(20f, 0f);

            Append(label);
            dragRectangle = new Rectangle(6, 6, (int)CWidth - 2, (int)CHeight - 6);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            float quotient = Utils.Clamp((float)Math.Floor(_cleanAverageEnergy.Sum() / 15f) / dbtPlayer.MaxOverload, 0, 1);

            Rectangle hitBox = GetInnerDimensions().ToRectangle();
            hitBox.X += dragRectangle.X;
            hitBox.Y += dragRectangle.Y;

            hitBox.Width = dragRectangle.Width;
            hitBox.Height = dragRectangle.Height;

            frameTimer++;

            if (frameTimer >= 12)
                frameTimer = 0;

            int frameHeight = 0;
            int frame = frameTimer / 5;

            Vector2 textureOffset = Vector2.Zero;

            // TODO !!IMPORTANT!! CHANGE THIS TO USE OBJECTS 100%.
            Texture = DBTMod.Instance.GetTexture("UserInterfaces/OverloadBar/OverloadBarFrame");

            frameHeight = Texture.Height / 4;
            textureOffset = new Vector2(16, 8);

            Position = new Vector2(hitBox.X - 6, hitBox.Y - 6);

            Rectangle sourceRectangle = new Rectangle(0, frameHeight * frame, Texture.Width, frameHeight);
            spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White);

            Texture2D barSegmentTexture = DBTMod.Instance.GetTexture("UserInterfaces/OverloadBar/OverloadBar");

            int segmentsCount = (int)Math.Ceiling(Segments * quotient);

            for (int i = 0; i < segmentsCount; i++)
            {
                Vector2 segmentOffsetX = new Vector2(i * 12, -2);
                Vector2 segmentPosition = Position + textureOffset + segmentOffsetX;

                if (i == segmentsCount - 1)
                {
                    float segmentValue = 1f / Segments;
                    float segmentRemainder = quotient % segmentValue;
                    float segmentQuotient = segmentRemainder / segmentValue;

                    if (segmentQuotient == 0f)
                        segmentQuotient = 1f;

                    spriteBatch.Draw(barSegmentTexture, segmentPosition, new Rectangle(0, 0, (int)Math.Ceiling(barSegmentTexture.Width * segmentQuotient), barSegmentTexture.Height), Color.White);
                }
                else
                    spriteBatch.Draw(barSegmentTexture, segmentPosition, new Rectangle(0, 0, barSegmentTexture.Width, barSegmentTexture.Height), Color.White);
            }
        }
        public override void Update(GameTime gameTime)
        {
            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            _cleanAverageEnergy.Add(dbtPlayer.Overload);

            if (_cleanAverageEnergy.Count > 15)
                _cleanAverageEnergy.RemoveRange(0, _cleanAverageEnergy.Count - 15);

            int averageOverload = (int)Math.Floor(_cleanAverageEnergy.Sum() / 15f);
            label.SetText("Overload: " + averageOverload + " / " + dbtPlayer.MaxOverload);

            base.Update(gameTime);
        }

        public float CWidth { get; }

        public float CHeight { get; }


        public int Segments { get; }


        public Vector2 Position { get; set; }

        public Texture2D Texture { get; set; }


    }
}
