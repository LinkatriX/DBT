using Terraria.ModLoader;

namespace DBT.Skills
{
    public abstract class GuardianProjectile : ModProjectile
    {
        protected GuardianProjectile(GuardianDefinition definition, int width, int height)
        {
            Definition = definition;
        }

        public GuardianDefinition Definition { get; }
    }
}