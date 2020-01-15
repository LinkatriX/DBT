using Microsoft.Xna.Framework;

namespace DBT.Skills.KiBlast
{
    public sealed class KiBlastDefinition : SkillDefinition
    {
        public KiBlastDefinition(params SkillDefinition[] parents) : base("KiBlast", "Ki Blast", "A small Ki blast that\ndamages enemies.", typeof(KiBlastItem), new KiBlastCharacteristics(), new Vector2(366, 202), "Given through Master Roshi's\ntutorial quest.", parents: parents) //28 Pixel padding on the X menu offset
        {
        }
    }

    public sealed class KiBlastCharacteristics : SkillCharacteristics
    {
        public KiBlastCharacteristics() : base(new KiBlastChargeCharacteristics(), 4, 1f, 15f, 3f, 1f, 0.05f, 1, 2, 1)
        {
        }
    }

    public sealed class KiBlastChargeCharacteristics : SkillChargeCharacteristics
    {
        public KiBlastChargeCharacteristics() : base(0, 0, 15, 0)
        {
        }
    }
}