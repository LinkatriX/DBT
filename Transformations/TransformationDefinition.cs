using System;
using System.Collections.Generic;
using DBT.Dynamicity;
using DBT.Players;
using DBT.Races;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using WebmilioCommons.Extensions;
using WebmilioCommons.Managers;

namespace DBT.Transformations
{
    public abstract class TransformationDefinition : IHasUnlocalizedName, IHasParents<TransformationDefinition>
    {
        internal const int TRANSFORMATION_LONG_DURATION = 6666666;

        private readonly List<RaceDefinition> _limitedToRaces;

        protected TransformationDefinition(string unlocalizedName, string displayName, Type buffType,
            float baseDamageMultiplier, float baseSpeedMultiplier, int baseDefenseAdditive,
            TransformationDrain drain, TransformationAppearance appearance, TransformationOverload? overload = null,
            bool mastereable = true, float maxMastery = 1f,
            int duration = TRANSFORMATION_LONG_DURATION, bool displaysInMenu = true, RaceDefinition[] limitedToRaces = null,
            bool anyParents = false, bool isManualLookup = false, string manualHairLookup = null, params TransformationDefinition[] parents)
        {
            UnlocalizedName = unlocalizedName;
            ManualHairLookup = manualHairLookup;
            DisplayName = displayName;

            BuffType = buffType;
            
            BaseDamageMultiplier = baseDamageMultiplier;
            BaseSpeedMultiplier = baseSpeedMultiplier;
            BaseDefenseAdditive = baseDefenseAdditive;

            Appearance = appearance;

            Mastereable = mastereable;
            BaseMaxMastery = maxMastery;

            Drain = drain;

            if (!overload.HasValue)
                overload = TransformationOverload.Zero;

            Overload = overload.Value;

            Duration = duration;

            DisplayInMenu = displaysInMenu;

            if (limitedToRaces != null)
                _limitedToRaces = new List<RaceDefinition>(limitedToRaces);
            else 
                _limitedToRaces = new List<RaceDefinition>();

            AnyParents = anyParents;

            IsManualLookup = isManualLookup;

            Parents = parents;
        }

        #region Methods

        #region Player Hooks Active

        public virtual void OnPlayerTransformed(DBTPlayer dbtPlayer, PlayerTransformation transformation) { }

        public virtual void OnPlayerMasteryChanged(DBTPlayer dbtPlayer, float change, float currentMastery) { }

        public virtual void OnActivePlayerDied(DBTPlayer dbtPlayer, double damage, bool pvp, PlayerDeathReason damageSource) { }

        public virtual void OnActivePlayerKilledNPC(DBTPlayer dbtPlayer, NPC npc) { }

        #endregion

        #region Player Hooks Acquired

        public virtual void OnPlayerLoading(DBTPlayer dbtPlayer, TagCompound tag) { }

        public virtual void OnPlayerSaving(DBTPlayer dbtPlayer, TagCompound tag) { }

        public virtual void OnPlayerAcquiredTransformation(DBTPlayer dbtPlayer) { }

        #endregion

        #region Player Hooks PreAcquired

        public virtual void OnPreAcquirePlayerKilledNPC(DBTPlayer dbtPlayer, NPC npc) { }

        public virtual void OnPreAcquirePlayerDied(DBTPlayer dbtPlayer, double damage, bool pvp, PlayerDeathReason damageSource) { }

        public virtual void OnPreAcquirePlayerLoading(DBTPlayer dbtPlayer, TagCompound tag) { }

        public virtual void OnPreAcquirePlayerSaving(DBTPlayer dbtPlayer, TagCompound tag) { }

        #endregion


        #region Access

        /// <summary>Called in special cases when the mod needs to know wether or not, regardless of the player, this transformation should work.</summary>
        /// <returns></returns>
        public virtual bool CheckPrePlayerConditions() => true;

        public bool HasParents(DBTPlayer dbtPlayer)
        {
            for (int i = 0; i < Parents.Length; i++)
            {
                if (AnyParents && dbtPlayer.AcquiredTransformations.ContainsKey(Parents[i]))
                    return true;

                if (!AnyParents && !dbtPlayer.AcquiredTransformations.ContainsKey(Parents[i]))
                    return false;
            }

            return true;
        }

        public bool BaseConditions(DBTPlayer dbtPlayer) => _limitedToRaces.Count == 0 || _limitedToRaces.Contains(dbtPlayer.Race);

        public bool CanUnlock(DBTPlayer dbtPlayer) => CheckPrePlayerConditions() && BaseConditions(dbtPlayer) && HasParents(dbtPlayer);

        /// <summary>Checks wether or not the transformation is part of the character menu. If not overriden, uses the same value as <see cref="CheckPrePlayerConditions"/>.</summary>
        /// <param name="dbtPlayer"></param>
        /// <returns></returns>
        public bool DoesDisplayInCharacterMenu(DBTPlayer dbtPlayer) => CheckPrePlayerConditions() && DisplayInMenu && BaseConditions(dbtPlayer);

        #endregion

        #region Multipliers

        public virtual float GetDamageMultiplier(DBTPlayer dbtPlayer) => BaseDamageMultiplier;

        public virtual float GetSpeedMultiplier(DBTPlayer dbtPlayer) => BaseSpeedMultiplier;

        #endregion

        #region Additive

        public virtual int GetDefenseAdditive(DBTPlayer dbtPlayer) => BaseDefenseAdditive;

        #endregion

        /*#region Ki

        public virtual float GetBaseKiAmount(DBTPlayer dbtPlayer) => dbtPlayer.BaseMaxKi;

        #endregion*/

        #region Ki Drain

        public virtual float GetUnmasteredKiDrain(DBTPlayer dbtPlayer) => Drain.baseUnmasteredKiDrain;

        public virtual float GetMasteredKiDrain(DBTPlayer dbtPlayer) => Drain.baseMasteredKiDrain;

        #endregion

        #region Health Drain

        public float GetUnmasteredHealthDrain(DBTPlayer dbtPlayer) => Drain.baseUnmasteredHealthDrain;

        public float GetMasteredHealthDrain(DBTPlayer dbtPlayer) => Drain.baseMasteredHealthDrain;

        #endregion

        #region Mastery

        public float GetCurrentMastery(DBTPlayer dbtPlayer)
        {
            if (dbtPlayer.HasAcquiredTransformation(this))
                return dbtPlayer.AcquiredTransformations[this].CurrentMastery;

            return 0f;
        }

        public virtual float GetMaxMastery(DBTPlayer dbtPlayer) => BaseMaxMastery;

        #endregion

        # region Overload

        public float GetUnmasteredOverloadGrowthRate(DBTPlayer dbtPlayer) => Overload.baseOverloadGrowthRate;

        public float GetMasteredOverloadGrowthRate(DBTPlayer dbtPlayer) => Overload.masteredOverloadGrowthRate;


        #endregion


        #endregion


        #region Properties

        public string UnlocalizedName { get; }

        public string ManualHairPath { get; set; }

        public bool IsManualLookup { get; set; }

        public string DisplayName { get; }

        public Type BuffType { get; }

        #region Statistics

        #region Multipliers

        public virtual float BaseDamageMultiplier { get; }

        public virtual float BaseSpeedMultiplier { get; }

        #endregion

        #region Additives

        public virtual int BaseDefenseAdditive { get; }

        #endregion

        #region Mastery

        public virtual bool Mastereable { get; }

        public float BaseMaxMastery { get; }

        #endregion

        public TransformationDrain Drain { get; }

        public TransformationOverload Overload { get; }

        #endregion

        public TransformationAppearance Appearance { get; }

        public int Duration { get; }

        public bool DisplayInMenu { get; }
         
        public string ManualHairLookup { get; set; }

        public IReadOnlyList<RaceDefinition> LimitedToRaces => _limitedToRaces.AsReadOnly();


        public bool AnyParents { get; }

        public TransformationDefinition[] Parents { get; }


        public virtual Texture2D TransformationIcon => BuffType.GetTexture();

        public virtual string TabHoverText { get; protected set; }

        #endregion
    }
}
