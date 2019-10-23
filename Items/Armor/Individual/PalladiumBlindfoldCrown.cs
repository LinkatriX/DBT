using DBT.Commons.Projectiles;
using DBT.Items.KiOrbs;
using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Individual
{
    [AutoloadEquip(EquipType.Head)]
    public sealed class PalladiumBlindfoldCrown : DBTArmorPiece, IHandleProjectileOnHitNPC
    {
        public PalladiumBlindfoldCrown() : base("Palladium Blindfold Crown",
            "9% Increased Ki Damage" +
            "\n6% Increased Ki Crit Chance",
            20, 16, Item.buyPrice(silver: 16), 5, ItemRarityID.LightRed)
        {
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => base.IsArmorSet(head, body, legs) && body.type == ItemID.PalladiumBreastplate && legs.type == ItemID.PalladiumLeggings;

        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);

            player.setBonus = "Hitting an enemy has a chance to drop Ki orbs.";
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.09f;
            dbtPlayer.KiCritAddition += 6;
        }

        public void OnProjectileHitNPC(ModProjectile projectile, NPC npc, ref int damage, ref float knockback, ref bool crit)
        {
            if (IsArmorSet(Main.player[projectile.projectile.owner]))
            {
                int kiOrb = 0;

                if (Main.rand.Next(10) == 0)
                    kiOrb = Item.NewItem((int) npc.position.X, (int) npc.position.Y, npc.width, npc.height, ModContent.ItemType<KiOrb>());

                if (Main.netMode == 1 && kiOrb >= 0)
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, kiOrb, 1f, 0f, 0f, 0, 0, 0);
            }
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            base.DrawHair(ref drawHair, ref drawAltHair);

            drawHair = true;
            drawAltHair = true;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.PalladiumBar, 10);
            recipe.AddTile(TileID.Anvils);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}