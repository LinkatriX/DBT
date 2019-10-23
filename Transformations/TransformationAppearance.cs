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
}
