﻿using System.Collections.Generic;
using DBT.HairStyles;
using DBT.UserInterfaces.HairMenu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private Color? _originalHairColor = null;

        internal void PreUpdateMovementHandleHair()
        {
            if (_originalHairColor.HasValue && FirstTransformation == null)
            {
                player.hairColor = _originalHairColor.Value;
                _originalHairColor = null;
            }
            else if (FirstTransformation != null && !_originalHairColor.HasValue)
            {
                _originalHairColor = player.hairColor;
            }

            if (FirstTransformation != null && FirstTransformation.Definition.Appearance.Hair != null && FirstTransformation.Definition.Appearance.Hair.Color.HasValue)
                player.hairColor = FirstTransformation.Definition.Appearance.Hair.Color.Value;

            if (ChosenHairStyle == null) return;

            if (FirstTransformation == null && CurrentHair != ChosenHairStyle.Base)
                CurrentHair = ChosenHairStyle.Base;
            else if (FirstTransformation != null && CurrentHair != ChosenHairStyle[FirstTransformation.Definition])
                CurrentHair = ChosenHairStyle[FirstTransformation.Definition];

            if (!HairChecked && player == Main.LocalPlayer)
                HairMenu.menuVisible = true;
        }

        internal void HandleHairDrawLayers(List<PlayerLayer> layers)
        {
            bool hasCustomHair = CurrentHair != null;

            int hairLayer = layers.FindIndex(l => l == PlayerLayer.Hair);
            if (hairLayer < 0) return;

            if (hasCustomHair)
                layers.Add(HairPlayerLayer.hairLayer);

            PlayerLayer.Head.visible = !hasCustomHair;
            PlayerLayer.Hair.visible = !hasCustomHair;
            PlayerLayer.HairBack.visible = !hasCustomHair;
            PlayerHeadLayer.Hair.visible = !hasCustomHair;
            PlayerHeadLayer.Head.visible = !hasCustomHair;

            HairPlayerLayer.hairLayer.visible = hasCustomHair;
        }

        public bool HairChecked { get; internal set; } = false;

        public HairStyle ChosenHairStyle { get; internal set; }

        public Texture2D CurrentHair { get; private set; }
    }
}
