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
        public bool firstDragonBallPickup { get; set; } 
        public bool isHoldingDragonRadarMk1 { get; set; }
        public bool isHoldingDragonRadarMk2 { get; set; }
        public bool isHoldingDragonRadarMk3 { get; set; }
    }
}
