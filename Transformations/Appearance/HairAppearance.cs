using Microsoft.Xna.Framework;

namespace DBT.Transformations.Appearance
{
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