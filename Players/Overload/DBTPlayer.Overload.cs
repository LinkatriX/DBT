using DBT.UserInterfaces.OverloadBar;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private float _overload;

        private void ResetOverloadEffects()
        {
            MaxOverload = 100 * Constants.TICKS_PER_SECOND;
            OverloadDecayRate = 20;
        }

        private void PreUpdateOverload()
        {

        }

        private void PostUpdateOverload()
        {
            if (IsTransformed())
            {
                float overloadGain = 0f;

                ForAllActiveTransformations(t => t.DoesTransformationOverload(this), t => overloadGain += t.Overload.GetOverloadGrowthRate(this));

                if (Overload + overloadGain > MaxOverload)
                    Overload = MaxOverload;
                else
                    Overload += overloadGain;
            }
            if (Overload > 0)
                DBTMod.Instance.overloadBar.Visible = true;
            else
                DBTMod.Instance.overloadBar.Visible = false;
        }


        public float OverloadDecayRate { get; set; }

        public float Overload
        {
            get => _overload;
            set
            {
                _overload = value;
                ForAllActiveTransformations(t => t.DoesTransformationOverload(this), t => t.Overload.OnPlayerOverloadUpdated(this, Overload, MaxOverload));
            }
        }

        public float MaxOverload { get; set; }
    }
}
