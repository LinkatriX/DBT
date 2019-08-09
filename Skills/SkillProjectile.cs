using DBT.Projectiles;

namespace DBT.Skills
{
    public abstract class SkillProjectile : KiProjectile
    {
        protected SkillProjectile(SkillDefinition definition, int width, int height) : 
            base(definition.Characteristics.BaseDamage, definition.Characteristics.BaseKnockback, width, height)
        {
            Definition = definition;
        }

        public SkillDefinition Definition { get; }
    }
}