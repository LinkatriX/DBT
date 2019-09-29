using DBT.Players;

namespace DBT.Auras
{
    public class LightingAppearance
    {
        public LightingAppearance(float[] baseRGBLightingRadiuses)
        {
            BaseRGBLightingRadiuses = baseRGBLightingRadiuses;
        }

        public virtual float[] GetRGBLightingRadiuses(DBTPlayer dbtPlayer)
        {
            if (!dbtPlayer.Charging)
                return BaseRGBLightingRadiuses;

            return new float[]
            {
                BaseRGBLightingRadiuses[0] * 1.3f,
                BaseRGBLightingRadiuses[1] * 1.3f,
                BaseRGBLightingRadiuses[2] * 1.3f
            };
        }

        public float[] BaseRGBLightingRadiuses { get; }
    }
}
