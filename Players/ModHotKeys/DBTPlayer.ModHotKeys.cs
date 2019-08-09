using DBT.Commons.Buffs;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (DBTMod.Instance.energyChargeKey.Current)
            {
                bool canCharge = true;

                for (int i = 0; i < player.buffType.Length; i++)
                {
                    ModBuff modBuff = BuffLoader.GetBuff(player.buffType[i]);
                    if (modBuff == null) continue;

                    ICanStopCharging icsc = modBuff as ICanStopCharging;
                    if (icsc == null) continue;

                    if (icsc.DoesStopCharging(this))
                    {
                        canCharge = false;
                        break;
                    }
                }

                IsCharging = canCharge;
            }
            else
                IsCharging = false;

            MouseLeftHeld = triggersSet.MouseLeft;
            MouseRightHeld = triggersSet.MouseRight;

            UpHeld = triggersSet.Up;
            DownHeld = triggersSet.Down;
            LeftHeld = triggersSet.Left;
            RightHeld = triggersSet.Right;

            if (player.whoAmI == Main.myPlayer)
            {
                if (DBTMod.Instance.transformUpKey.JustPressed && SelectedTransformations != null)
                    TryTransforming(SelectedTransformations);

                if (DBTMod.Instance.transformDownKey.JustPressed)
                    ClearTransformations();

                if (DBTMod.Instance.characterMenuKey.JustPressed)
                    DBTMod.Instance.characterTransformationsMenu.Visible = !DBTMod.Instance.characterTransformationsMenu.Visible;
                if (DBTMod.Instance.flightToggleKey.JustPressed)
                    Flying = !Flying;
            }
        }

        public bool MouseLeftHeld { get; set; }
        public bool MouseRightHeld { get; set; }

        public bool UpHeld { get; set; }
        public bool DownHeld { get; set; }
        public bool LeftHeld { get; set; }
        public bool RightHeld { get; set; }
    }
}
