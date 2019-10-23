using DBT.Transformations;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Players
{
    public sealed partial class DBTPlayer : ModPlayer
    {
        public void OnKilledNPC(NPC npc)
        {
            TransformationDefinitionManager.Instance.ForAllItems(t => t.OnPreAcquirePlayerKilledNPC(this, npc));
            ForAllActiveTransformations(t => t.OnActivePlayerKilledNPC(this, npc));

            UpdateProgression(npc);
        }

        public override void OnEnterWorld(Player player)
        {
            HandleTransformationsOnEnterWorld(player);
            HandleSkillsOnEnterWorld(player);
        }

        public bool PlayerInitialized { get; private set; }
        public float HealthDrainMultiplier { get; set; }
        public int ChargeLimitAdd { get; private set; }
        public float CurrentKiAttackChargeLevel { get; set; } = 0f;
        public float CurrentKiAttackMaxChargeLevel { get; set; } = 0f;

        //Temporary for testing, this garbage will be taken out later
        public float kiDamage;
        public float kiKbAddition;
        public float kiSpeedAddition;
        public int kiCrit;
        public int kiRegenTimer;
        public int kiRegen;
        public int kaiokenLevel = 0;
        public float kiDamageMulti = 1f;
        public float kiDrainMulti;
        public bool isHoldingKiWeapon;
    }
}
