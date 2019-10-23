using DBT.Players;

namespace DBT.Auras
{
    public class AuraAppearance
    {
        public AuraAppearance(AuraAnimationInformation information, LightingAppearance lighting)
        {
            Information = information;
            Lighting = lighting;
        }

        public virtual int GetTicksPerFrameTimerTick(DBTPlayer dbtPlayer)
        {
            if (dbtPlayer.Charging)
                return Information.TicksPerFrameTimerTick * 2;

            return Information.TicksPerFrameTimerTick;
        }

        public AuraAnimationInformation Information { get; }

        public LightingAppearance Lighting { get; }
    }
}