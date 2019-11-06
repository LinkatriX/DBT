using System;
using DBT.Auras;
using DBT.Helpers;
using DBT.Players;
using DBT.Transformations.Appearance;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader.IO;

namespace DBT.Transformations.Developers.Webmilio
{
    public sealed class SoulStealerTransformation : TransformationDefinition
    {
        private const string
            SOULSTEALER_PREFIX = "SoulStealer_",
            SOULPOWER_TAG = SOULSTEALER_PREFIX + "SoulPowerCount";

        public SoulStealerTransformation() : base(
            "SoulStealer", "Soul Stealer", typeof(SoulStealerTransformationBuff),
            1f, 1f, 0,
            new TransformationDrain(0f, 0f), 
            new SoulStealerAppearance(),
            new TransformationOverload(0, 0))
        {
        }

        #region Loading/Saving

        public override void OnPreAcquirePlayerLoading(DBTPlayer dbtPlayer, TagCompound tag)
        {
            base.OnPlayerLoading(dbtPlayer, tag);
            DefaultSetup(dbtPlayer);
        }

        public override void OnPreAcquirePlayerSaving(DBTPlayer dbtPlayer, TagCompound tag)
        {
            base.OnPlayerSaving(dbtPlayer, tag);
            DefaultSetup(dbtPlayer);
        }

        #endregion

        private void DefaultSetup(DBTPlayer dbtPlayer)
        {
            if (!CheckPrePlayerConditions())
            {
                dbtPlayer.AcquiredTransformations.Remove(this);
                return;
            }

            if (!dbtPlayer.HasAcquiredTransformation(this))
            {
                dbtPlayer.Acquire(this);
            }
        }



        public override void OnActivePlayerKilledNPC(DBTPlayer dbtPlayer, NPC npc) => AddSoulPower(dbtPlayer, npc);

        public override float GetDamageMultiplier(DBTPlayer dbtPlayer)
        {
            return BaseDamageMultiplier + GetSoulPower(dbtPlayer) / 500f;
        }

        public override float GetSpeedMultiplier(DBTPlayer dbtPlayer)
        {
            return BaseSpeedMultiplier + GetSoulPower(dbtPlayer) / 500f;
        }

        public override int GetDefenseAdditive(DBTPlayer dbtPlayer)
        {
            return (int)Math.Round(GetSoulPower(dbtPlayer) / 50f);
        }


        public override bool CheckPrePlayerConditions() => SteamHelper.CurrentUser == SteamHelper.Webmilio;


        public float GetSoulPower(DBTPlayer dbtPlayer)
        {
            PlayerTransformation playerTransformation = dbtPlayer.AcquiredTransformations[this];

            if (!playerTransformation.ExtraInformation.ContainsKey(SOULPOWER_TAG))
                playerTransformation.ExtraInformation.Add(SOULPOWER_TAG, 0f);

            return (float) playerTransformation.ExtraInformation[SOULPOWER_TAG];
        }

        public void SetSoulPower(DBTPlayer dbtPlayer, float multiplier) => dbtPlayer.AcquiredTransformations[this].ExtraInformation[SOULPOWER_TAG] = multiplier;

        public void AddSoulPower(DBTPlayer dbtPlayer, NPC npc)
        {
            float divide = GetSoulPower(dbtPlayer) * 0.3f;
            float gain = npc.lifeMax * 0.42f;

            if (divide > 0)
                gain /= divide;

            if (gain < 1)
                gain = 1;

            if (npc.boss)
                gain *= 2;

            SetSoulPower(dbtPlayer, GetSoulPower(dbtPlayer) + gain);
        }
    }

    public sealed class SoulStealerTransformationBuff : TransformationBuff
    {
        public SoulStealerTransformationBuff() : base(TransformationDefinitionManager.Instance.SoulStealer)
        {
        }

        public override string BuildDefaultTooltip(DBTPlayer player)
        {
            return base.BuildDefaultTooltip(player) + "\nSoul Power: " + ((SoulStealerTransformation)Definition).GetSoulPower(player);
        }
    }

    public sealed class SoulStealerAppearance : TransformationAppearance
    {
        public SoulStealerAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SoulStealerTransformation), 8, 3, BlendState.Additive, true), 
                new LightingAppearance(new float[] { 0.85f, 0f, 1.30f })),
            new HairAppearance(Color.Fuchsia), Color.Fuchsia, Color.Fuchsia)
        {
        }
    }
}
