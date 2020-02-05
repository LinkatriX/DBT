using Microsoft.Xna.Framework;

namespace DBT.Skills.TrapShooter
{
    public sealed class TrapShooterDefinition : SkillDefinition
    {
        public TrapShooterDefinition(params SkillDefinition[] parents) : base("TrapShooter", "Trap Shooter", "A ki blast.", typeof(TrapShooterItem), new TrapShooterCharacteristics(), new Vector2(196, 192), parents: parents)
        {
        }
    }

    public sealed class TrapShooterCharacteristics : SkillCharacteristics
    {
        public TrapShooterCharacteristics() : base(new TrapShooterChargeCharacteristics(), 82, 1f, 35f, 3f, 1f)
        {
        }
    }

    public sealed class TrapShooterChargeCharacteristics : SkillChargeCharacteristics
    {
        public TrapShooterChargeCharacteristics() : base(0, 0, 115, 0) 
        {
        }
    }
}
