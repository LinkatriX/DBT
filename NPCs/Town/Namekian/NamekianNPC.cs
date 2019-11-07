using DBT.NPCs.Town.Roshi;
using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WebmilioCommons.Tinq;

namespace DBT.NPCs.Town.Namekian
{
	[AutoloadHead]
	public class NamekianNPC : ModNPC
	{
        public override string Texture => "DBT/NPCs/Town/Namekian/NamekianNPC";

        public override string[] AltTextures => new[] { "DBT/NPCs/Town/Namekian/NamekianNPC_Alt_1" };

        public override bool Autoload(ref string name)
		{
			name = "Namekian";
			return mod.Properties.Autoload;
		}

		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Namekian");
            Main.npcFrameCount[npc.type] = 22;
			NPCID.Sets.ExtraFramesCount[npc.type] = 9;
			NPCID.Sets.AttackFrameCount[npc.type] = 3;
			NPCID.Sets.DangerDetectRange[npc.type] = 700;
			NPCID.Sets.AttackType[npc.type] = 2;
			NPCID.Sets.AttackTime[npc.type] = 90;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
			NPCID.Sets.HatOffsetY[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 18;
			npc.height = 40;
			npc.aiStyle = 7;
			npc.damage = 30;
			npc.defense = 20;
			npc.lifeMax = 500;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.5f;
			animationType = NPCID.Guide;
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money) =>
            Main.player.AnyActive(p => p.GetModPlayer<RoshiQuests>().QuestsCompleted >= 3);

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(8))
            {
                case 0:
                    return "Pirina";
                case 1:
                    return "Moori";
                case 2:
                    return "Partul";
                case 3:
                    return "Ghast";
                case 4:
                    return "Tsuno";
                case 5:
                    return "Katas";
                case 6:
                    return "Ushi";
                case 7:
                    return "Limax";
                default:
                    return "Saonel";
            }
        }

        public override string GetChat()
		{
            DBTPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();
            Player player = Main.LocalPlayer;
			switch (Main.rand.Next(3))
			{
				case 0:
					return "I do wonder how I've gotten here, I quite miss planet Namek.";
				case 1:
					return "I may be able to teach you some ancient Namekian techniques if you wish. I will want some compensation however.";
				default:
					return "The Dragon Balls? Our people are the ones who invented those you know, and no you can't have ours.";
			}
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            shop = true;
        }
        
        /*public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(mod.ItemType("KiBlast"));
            shop.item[nextSlot].value = 10000;
			nextSlot++;
			if (NPC.downedBoss2)
			{
				shop.item[nextSlot].SetDefaults(mod.ItemType("Kamehameha"));
                shop.item[nextSlot].value = 30000;
				nextSlot++;
			}
            if (NPC.downedQueenBee)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("HermitGi"));
                shop.item[nextSlot].value = 50000;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("HermitPants"));
                shop.item[nextSlot].value = 50000;
                nextSlot++;
            }
        }*/

        private int GetWeaponProgression()
        {
            if(Main.hardMode)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
            switch(GetWeaponProgression())
            {
                case 1:
                    damage = 64;
                    knockback = 3f;
                    break;
                case 0:
                    damage = 26;
                    knockback = 2f;
                    break;
            }
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
            switch (GetWeaponProgression())
            {
                case 1:
                    cooldown = 16;
                    randExtraCooldown = 6;
                    break;
                case 0:
                    cooldown = 24;
                    randExtraCooldown = 8;
                    break;
            }
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
            switch (GetWeaponProgression())
            {
                case 1:
                    projType = mod.ProjectileType("EnergyShot");
                    attackDelay = 15;
                    break;
                case 0:
                    projType = mod.ProjectileType("KiBlast");
                    attackDelay = 20;
                    break;
            }
            
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
            switch (GetWeaponProgression())
            {
                case 1:
                    multiplier = 18f;
                    break;
                case 0:
                    multiplier = 13f;
                    break;
            }
		}
    }
}