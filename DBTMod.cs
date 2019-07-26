using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using System;
using DBT.Helpers;
using DBT.Network;
using DBT.Players;
using DBT.UserInterfaces.CharacterMenus;
using DBT.UserInterfaces.KiBar;
using DBT.UserInterfaces.OverloadBar;
using DBT.UserInterfaces;
using DBT.Effects;
using Microsoft.Xna.Framework.Graphics;
using DBT.UserInterfaces.KiAttackUI;

namespace DBT
{
    public sealed class DBTMod : Mod
    {
        internal ModHotKey characterMenuKey, energyChargeKey, transformDownKey, speedToggleKey, transformUpKey, flightToggleKey, instantTransmission;

        internal KiBar kiBar;
        internal UserInterface kiBarInterface;

        internal static CircleShader circle;
        internal KiBrowserUIMenu kiBrowserMenu;
        internal UserInterface kiBrowserInterface;
        internal OverloadBar overloadBar;
        internal UserInterface overloadBarInterface;
        internal DBTMenu dbtMenu;
        internal CharacterTransformationsMenu characterTransformationsMenu;
        internal UserInterface characterMenuInterface;

        public DBTMod()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };

            Instance = this;
        }

        public override void Load()
        {
            if (!Main.dedServ)
            {
                SteamHelper.Initialize();

                #region HotKeys

                characterMenuKey = RegisterHotKey("Character Menu", "K");
                energyChargeKey = RegisterHotKey("Energy Charge", "C");
                speedToggleKey = RegisterHotKey("Speed Toggle", "Z");
                transformDownKey = RegisterHotKey("Transform Down", "V");
                transformUpKey = RegisterHotKey("Transform Up", "X");
                flightToggleKey = RegisterHotKey("Flight Toggle", "Q");
                instantTransmission = RegisterHotKey("Instant Transmission", "I");

                #endregion

                kiBar = new KiBar();
                kiBar.Activate();

                kiBarInterface = new UserInterface();
                kiBarInterface.SetState(kiBar);
                kiBarInterface = new UserInterface();
                kiBarInterface.SetState(kiBar);

                kiBar.Visible = true;

                overloadBar = new OverloadBar();
                overloadBar.Activate();
                overloadBarInterface = new UserInterface();
                overloadBarInterface.SetState(overloadBar);

                dbtMenu = new DBTMenu();
                dbtMenu.Activate();

                kiBrowserMenu = new KiBrowserUIMenu();
                kiBrowserMenu.Activate();
                kiBrowserInterface = new UserInterface();
                kiBrowserInterface.SetState(kiBrowserMenu);

                characterTransformationsMenu = new CharacterTransformationsMenu(this);
                characterTransformationsMenu.Activate();
                characterMenuInterface = new UserInterface();
                characterMenuInterface.SetState(characterTransformationsMenu);

                Properties = new ModProperties()
                {
                    Autoload = true,
                    AutoloadGores = true,
                    AutoloadSounds = true,
                    AutoloadBackgrounds = true
                };

                Instance = this;

                circle = new CircleShader(new Ref<Effect>(GetEffect("Effects/CircleShader")), "Pass1");

                kiBrowserMenu.Visible = true;
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                kiBar.Visible = false;

                kiBrowserMenu.Visible = false;

                characterTransformationsMenu.Visible = false;

                overloadBar.Visible = false;
            }

            Instance = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (characterMenuInterface != null && characterTransformationsMenu.Visible)
                characterMenuInterface.Update(gameTime);

            if (kiBrowserInterface != null && kiBrowserMenu.Visible)
                kiBrowserInterface.Update(gameTime);

        }

        [Obsolete]
        public override void UpdateMusic(ref int music)
        {
            int[] noOverride =
                {
                    MusicID.Boss1, MusicID.Boss2, MusicID.Boss3, MusicID.Boss4, MusicID.Boss5,
                    MusicID.LunarBoss, MusicID.PumpkinMoon, MusicID.TheTowers, MusicID.FrostMoon, MusicID.GoblinInvasion,
                    MusicID.Eclipse, MusicID.MartianMadness, MusicID.PirateInvasion,
                    GetSoundSlot(SoundType.Music, "Sounds/Music/TheUnexpectedArrival"),
                };

            int m = music;
            bool playMusic = !noOverride.Any(song => song == m) || !Main.npc.Any(npc => npc.boss);

            Player player = Main.LocalPlayer;

            if (player.active && player.GetModPlayer<DBTPlayer>(this).zoneWasteland && !Main.gameMenu && playMusic)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Wastelands");
            }
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI) => NetworkPacketManager.Instance.HandlePacket(reader, whoAmI);

        public override void PostSetupContent()
        {
            // Boss checklist support
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddBossWithInfo", "A Frieza Force Ship", 3.8f, (Func<bool>)(() => DBTWorld.downedFriezaShip), "Alert and let a frieza force scout escape in the wasteland biome after the world evil has been killed.");
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int
                resourcesLayerIndex = layers.FindIndex(l => l.Name.Contains("Resource Bars")),
                characterMenuIndex = layers.FindIndex(l => l.Name.Contains("Hotbar"));

            if (resourcesLayerIndex != -1)
            {
                layers.Insert(resourcesLayerIndex, new OverloadBarLayer());
                layers.Insert(resourcesLayerIndex, new KiBarLayer());
            }
            if (characterMenuIndex != -1)
            {
                layers.Insert(characterMenuIndex, new CharacterTransformationsMenuLayer(characterTransformationsMenu, characterMenuInterface));
                layers.Insert(characterMenuIndex, new KiBrowserLayer(kiBrowserMenu, kiBrowserInterface));
            }
        }
        public static uint GetTicks() => Main.GameUpdateCount;
        public static bool IsTickRateElapsed(int rateModulo) => GetTicks() > 0 && GetTicks() % rateModulo == 0;
        public static DBTMod Instance { get; set; }
    }
}
