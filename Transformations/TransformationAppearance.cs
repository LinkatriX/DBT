using DBT.Auras;
using Microsoft.Xna.Framework;

namespace DBT.Transformations
{
    public abstract class TransformationAppearance
    {
        protected TransformationAppearance(AuraAppearance aura, HairAppearance hair, Color? generalColor, Color? eyeColor)
        {
            Aura = aura;
            Hair = hair;
            EyeColor = eyeColor;
            GeneralColor = generalColor;
        }

        public AuraAppearance Aura { get; }

        public HairAppearance Hair { get; }

        public Color? GeneralColor { get; }

        public Color? EyeColor { get; }
    }

    public class HairAppearance
    {
        public HairAppearance(Color? color)
        {
            Color = color;
        }

        public Color? Color { get; }
    }
}
