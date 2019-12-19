using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.DeathSaucer
{
    public sealed class DeathSaucerProjectile : SkillProjectile
    {
        public DeathSaucerProjectile() : base(SkillDefinitionManager.Instance.DeathSaucer, 124, 192)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return true;
        }
    }
}