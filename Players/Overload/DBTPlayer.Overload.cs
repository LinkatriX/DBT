using DBT.Transformations;
using DBT.UserInterfaces.OverloadBar;

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

        private void PostUpdateOverload()
        {
            if (IsTransformed())
            {
                float overloadGain = 0f;

                for (int i = 0; i < ActiveTransformations.Count; i++)
                    if (ActiveTransformations[i].DoesTransformationOverload(this))
                        overloadGain += ActiveTransformations[i].Overload.GetOverloadGrowthRate();

                if (Overload + overloadGain > MaxOverload)
                    Overload = MaxOverload;
                else
                    Overload += overloadGain;
            }
            else if (!IsTransformed())
            {
                if (DBTMod.IsTickRateElapsed(180))
                {
                    int overloadDecreaseTimer = 0;
                    overloadDecreaseTimer++;

                    if (overloadDecreaseTimer >= OverloadDecayRate)
                    {
                        Overload--;
                        overloadDecreaseTimer = 0;
                    }
                }

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
                for (int i = 0; i < ActiveTransformations.Count; i++)
                    if (ActiveTransformations[i].DoesTransformationOverload(this))
                        ActiveTransformations[i].Overload.OnPlayerOverloadUpdated(this, Overload, MaxOverload);
            }
        }

        public float MaxOverload { get; set; }
    }
}
