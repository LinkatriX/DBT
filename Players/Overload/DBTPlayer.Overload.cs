using DBT.Projectiles.Overload;
using DBT.Transformations;
using DBT.UserInterfaces.OverloadBar;
using Microsoft.Xna.Framework;
using Terraria;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private float _overload;

        private void ResetOverloadEffects()
        {
            MaxOverload = 100;
            OverloadDecayRate = 5;
        }

        private void PreUpdateOverload()
        {

        }

        int overloadDecreaseTimer = 0;
        private void PostUpdateOverload()
        {
            if (!IsTransformed(TransformationDefinitionManager.Instance.SSJC) && !IsTransformed(TransformationDefinitionManager.Instance.LSSJ))
            {
                overloadDecreaseTimer++;
                if (overloadDecreaseTimer >= 180)
                {
                    if (DBTMod.IsTickRateElapsed(OverloadDecayRate))
                    {
                        Overload--;
                    }
                }
            }
            else
                overloadDecreaseTimer = 0;

            if (Overload >= MaxOverload && !IsOverloading)
                OnMaxOverload();

            if (Overload > MaxOverload)
                Overload = MaxOverload;

            if (Overload > 0)
                DBTMod.Instance.overloadBar.Visible = true;
            else
                DBTMod.Instance.overloadBar.Visible = false;
        }


        private void OnMaxOverload()
        {
            IsOverloading = true;
            if (IsTransformed(TransformationDefinitionManager.Instance.SSJC) && !HasAcquiredTransformation(TransformationDefinitionManager.Instance.LSSJ))
                AcquireAndTransform(TransformationDefinitionManager.Instance.LSSJ);
            else
                DoShaderEffects();
        }

        private void DoShaderEffects()
        {
            Projectile.NewProjectile(player.position.X, player.position.Y, 0, 0, mod.ProjectileType<ShaderOrb1>(), 0, 0, player.whoAmI);
        }
        public void DoOverloadOrb()
        {              
            Projectile.NewProjectile(player.position.X, player.position.Y, 0, 0, mod.ProjectileType<AuraOrb>(), 0, 0, player.whoAmI);
        }


        public int OverloadDecayRate { get; set; }

        public bool IsOverloading { get; set; } = false;

        public float Overload
        {
            get => _overload;
            set
            {
                _overload = value;
            }
        }

        public float MaxOverload { get; set; }
    }
}
