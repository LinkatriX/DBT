using System.Collections.Generic;
using DBT.Players;

namespace DBT.Transformations
{
    public class PlayerTransformation : IPlayerSavable
    {
        internal const string MASTERY_PREFIX = "Mastery_";

        private float _currentMastery;

        public PlayerTransformation(TransformationDefinition definition, float currentMastery = 0f)
        {
            Definition = definition;
            _currentMastery = currentMastery;
        }


        #region Player Hooks

        public void ChangeMastery(DBTPlayer player, float difference)
        {
            CurrentMastery += difference;
            Definition.OnPlayerMasteryChanged(player, difference, CurrentMastery);
        }

        #endregion


        public bool HasPlayerMastered(DBTPlayer dbtPlayer) => CurrentMastery >= Definition.GetMaxMastery(dbtPlayer);


        public KeyValuePair<string, object> ToSavableFormat() => new KeyValuePair<string, object>(MASTERY_PREFIX + Definition.UnlocalizedName, CurrentMastery);


        public TransformationDefinition Definition { get; }

        public float CurrentMastery { get; private set; }

        public Dictionary<string, object> ExtraInformation { get; } = new Dictionary<string, object>();
    }
}
