namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        internal void PostUpdateFlight()
        {
            if (!Flying) return;
        }

        public bool Flying { get; internal set; }

        public bool FlightUnlocked { get; set; }
        public bool FlightDampenedFall { get; set; }
        public bool FlightT3 { get; set; }

        public float FlightSpeedModifier { get; set; }
        public float FlightKiUsageModifier { get; set; }
    }
}