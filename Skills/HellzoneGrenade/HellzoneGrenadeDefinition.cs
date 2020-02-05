using Microsoft.Xna.Framework;

namespace DBT.Skills.HellzoneGrenade
{
    public sealed class HellzoneGrenadeDefinition : SkillDefinition
    {
        public HellzoneGrenadeDefinition(params SkillDefinition[] parents) : base("HellzoneGrenade", "Hellzone Grenade", "'It's even got a cool name.'\nFires a barrage of homing\nki blasts.", typeof(HellzoneGrenadeItem), new HellzoneGrenadeCharactersitics(), new Vector2(416, 112), "Unlocked after hitting 2000 max ki\nand killing 30 enemies with grenades.", parents: parents)
        {
        }
    }

    public sealed class HellzoneGrenadeCharactersitics : SkillCharacteristics
    {
        public HellzoneGrenadeCharactersitics() : base(new HellzoneGrenadeChargeCharacteristics(), 52, 1f, 17f, 6f, 1f)
        {
        }
    }

    public sealed class HellzoneGrenadeChargeCharacteristics : SkillChargeCharacteristics
    {
        public HellzoneGrenadeChargeCharacteristics() : base(0, 0, 75, 0)
        {
        }
    }
}
