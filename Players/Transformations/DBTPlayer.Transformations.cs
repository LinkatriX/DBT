using System;
using System.Collections.Generic;
using DBT.Network;
using DBT.Network.Transformations;
using DBT.Transformations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private void PostUpdateHandleTransformations()
        {
            PostUpdateHandleTransformationsVisuals();
            UpdateConditions();
            ListenForTransformations();
        }

        private void HandleTransformationsOnEnterWorld(Player player)
        {
            DBTMod.Instance.characterTransformationsMenu.OnPlayerEnterWorld(player.GetModPlayer<DBTPlayer>());
        }

        public void ListenForTransformations()
        {
            // TODO Add transformation management code.
            if (Ki <= 1)
            {
                Untransform(GetTransformation());
            }
        }


        public void AcquireAndTransform(TransformationDefinition definition)
        {
            Acquire(definition);
            Transform(definition);
        }

        public void Acquire(TransformationDefinition definition)
        {
            if (AcquiredTransformations.ContainsKey(definition)) return;

            AcquiredTransformations.Add(definition, new PlayerTransformation(definition));
            definition.OnPlayerAcquiredTransformation(this);
        }

        public void Transform(TransformationDefinition definition)
        {
            for (int i = 0; i < ActiveTransformations.Count; i++)
                if (ActiveTransformations[i] == definition)
                    return;

            ActiveTransformations.Add(definition);
            player.AddBuff(mod.GetBuff(definition.BuffType.Name).Type, definition.Duration);

            if (player.whoAmI == Main.myPlayer)
                new PlayerTransformedPacket(definition).Send();
        }

        public void TryTransforming(List<TransformationDefinition> transformations)
        {
            // TODO Add transformation checks here.
            for (int i = 0; i < transformations.Count; i++)
                Transform(transformations[i]);
        }


        public void Untransform(TransformationBuff transformation) => Untransform(transformation.Definition);

        public void Untransform(PlayerTransformation transformation)
        {
            if (!ActiveTransformations.Contains(transformation.Definition)) return;

            Untransform(transformation.Definition);
        }

        public void Untransform(TransformationDefinition definition)
        {
            for (int i = ActiveTransformations.Count - 1; i >= 0; i--)
            {
                TransformationDefinition transformation = ActiveTransformations[i];

                if (transformation == definition)
                {
                    ActiveTransformations.Remove(transformation);

                    if (player.whoAmI == Main.myPlayer)
                        new PlayerUntransformedPacket(transformation).Send();

                    player.ClearBuff(mod.GetBuff(definition.BuffType.Name).Type);
                }
            }
        }

        public void ClearTransformations()
        {
            for (int i = ActiveTransformations.Count - 1; i >= 0; i--)
                Untransform(ActiveTransformations[i]);
        }

        public bool IsTransformed() => ActiveTransformations.Count > 0;

        public bool IsTransformed(TransformationDefinition definition)
        {
            for (int i = 0; i < ActiveTransformations.Count; i++)
                if (ActiveTransformations.Contains(definition))
                    return true;

            return false;
        }

        public bool IsTransformed(TransformationBuff buff)
        {
            if (ActiveTransformations.Count == 0) return false;

            for (int i = 0; i < ActiveTransformations.Count; i++)
            {
                Type buffType = buff.GetType();
                bool isBuff = ActiveTransformations[i].BuffType.IsAssignableFrom(buffType);

                if (isBuff)
                    return true;
            }

            return false;
        }

        public bool HasAcquiredTransformation(TransformationDefinition definition)
        {
            for (int i = 0; i < AcquiredTransformations.Count; i++)
                if (AcquiredTransformations.ContainsKey(definition))
                    return true;

            return false;
        }   

        public bool HasMastered() => GetTransformation().HasPlayerMastered(this);

        public bool HasMastered(TransformationDefinition transformation) => AcquiredTransformations[transformation].HasPlayerMastered(this);


        public PlayerTransformation GetTransformation()
        {
            if (ActiveTransformations.Count == 0) return null;

            return AcquiredTransformations[ActiveTransformations[0]];
        }

        public bool TryCombiningTransformations(params TransformationDefinition[] transformations)
        {
            // TODO Add code for SSBKK and similar transformations.
            return false;
        }

        public void SelectTransformation(TransformationDefinition transformation)
        {
            if (!TryCombiningTransformations(transformation))
            {
                SelectedTransformations.Clear();
                SelectedTransformations.Add(transformation);
            }
            else 
                SelectedTransformations.Add(transformation);
        }

        public Color? originalEyeColor = null;

        public void ChangeEyeColor(Color eyeColor)
        {
            // only fire this when attempting to change the eye color.
            if (originalEyeColor == null)
            {
                originalEyeColor = player.eyeColor;
            }
            player.eyeColor = eyeColor;
        }

        public Dictionary<TransformationDefinition, PlayerTransformation> AcquiredTransformations { get; internal set; }
        public List<TransformationDefinition> ActiveTransformations { get; internal set; }

        public PlayerTransformation FirstTransformation { get; private set; }
        public List<TransformationDefinition> SelectedTransformations { get; internal set; }
    }
}
