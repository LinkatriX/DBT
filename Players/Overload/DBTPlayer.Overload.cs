using DBT.Buffs;
using DBT.Helpers;
using DBT.Projectiles.Overload;
using DBT.Transformations;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private float _overload;

        private void ResetOverloadEffects()
        {
            MaxOverload = 100;
            OverloadDecayRate = 5;
            OverloadIncreaseMultiplier = 1f;
        }

        private void PreUpdateOverload()
        {

        }

        int overloadDecreaseTimer = 0;
        private void PostUpdateOverload()
        {
            if (!IsTransformed(TransformationDefinitionManager.Instance.SSJA) && !IsTransformed(TransformationDefinitionManager.Instance.SSJC) && !IsTransformed(TransformationDefinitionManager.Instance.LSSJ))
            {
                overloadDecreaseTimer++;
                if (overloadDecreaseTimer >= 180)
                {
                    if (DBTMod.IsTickRateElapsed(OverloadDecayRate))
                    {
                        if (Overload > 0)
                            Overload--;
                    }
                }
            }
            else
                overloadDecreaseTimer = 0;

            if (IsOverloading)
                OverloadEffects();
            else if (!IsOverloading)
            {
                OverloadDamageMultiplier = 1f;
                if (DBTMod.IsTickRateElapsed(60))
                    if (OverloadKiMultiplier < 1f)
                        OverloadKiMultiplier += 0.01f;
                    
            }

            if (player.dead)
                Overload = 0;

            if (Overload >= MaxOverload && !IsOverloading)
                OnMaxOverload();

            if (Overload > MaxOverload)
                Overload = MaxOverload;

            if (DBTMod.Instance.overloadBar != null)
            {
                if (Overload > 0)
                    DBTMod.Instance.overloadBar.Visible = true;
                else
                    DBTMod.Instance.overloadBar.Visible = false;
            }

            if (IsOverloading && Overload <= 0)
            {
                IsOverloading = false;
                player.AddBuff(ModContent.BuffType<KiOverusage>(), 60 * 60);
            }
                
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
            Projectile.NewProjectile(player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<ShaderOrb1>(), 0, 0, player.whoAmI);
            SoundHelper.PlayCustomSound("Sounds/Overload/Overloadcircle", player, 0.3f);
        }
        public void DoOverloadOrb()
        {              
            Projectile.NewProjectile(player.position.X, player.position.Y, 0, 0, ModContent.ProjectileType<AuraOrb>(), 0, 0, player.whoAmI);
            SoundHelper.PlayCustomSound("Sounds/Overload/Overloadburst", player, 0.3f);
        }

        public void OverloadEffects()
        {
            if (DBTMod.IsTickRateElapsed(300))
                OverloadDamageMultiplier = Main.rand.NextFloat(0.25f, 2f);
            if (DBTMod.IsTickRateElapsed(120))
                OverloadKiMultiplier -= 0.01f;

            player.allDamage *= OverloadDamageMultiplier;
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

        public float OverloadKiMultiplier { get; set; } = 1f;

        public float OverloadDamageMultiplier { get; set; } = 1f;

        public float OverloadIncreaseMultiplier { get; set; }
    }
}
