using DBT.Auras;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;

namespace DBT.Transformations
{
    public abstract class TransformationAppearance
    {
        protected TransformationAppearance(AuraAppearance aura, HairAppearance hair, /*OnTransformationAnimation animation*/ Color? generalColor, Color? eyeColor)
        {
            Aura = aura;
            Hair = hair;
            //Animation = animation;
            //Animation = animation;
            EyeColor = eyeColor;
            GeneralColor = generalColor;
        }

        public AuraAppearance Aura { get; }

        public HairAppearance Hair { get; }

        public OnTransformationAnimation Animation { get; }

        public Color? GeneralColor { get; }

        public Color? EyeColor { get; }
    }

    public class HairAppearance
    {
        public HairAppearance(Color? color, TransformationDefinition manualForm = null, int manualFormOffsetX = 0, int manualFormOffsetY = 0)
        {
            Color = color;
            ManualForm = manualForm;
            ManualFormOffset = new Vector2(manualFormOffsetX, manualFormOffsetY);
        }

        public Color? Color { get; }

        public TransformationDefinition ManualForm { get; }

        public Vector2 ManualFormOffset { get; }
    }

    public class OnTransformationAnimation 
    {
        public OnTransformationAnimation(int frameCounterLimit, int numberOfFrames, int yOffset, TransformationDefinition transformation, PlayerDrawInfo drawInfo, Type transformationType) 
        {
            FrameCounterLimit = frameCounterLimit;
            NumberOfFrames = numberOfFrames;
            YOffset = yOffset;
            Transformation = transformation;
            DrawInfo = drawInfo;
            TransformationType = transformationType;
        }

        public int FrameCounterLimit { get; }

        public int NumberOfFrames { get; }

        public int YOffset { get; }

        public TransformationDefinition Transformation { get; }

        public PlayerDrawInfo DrawInfo { get; }

        public Type TransformationType { get; }
    }
}
