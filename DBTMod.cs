using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
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
using DBT.UserInterfaces.WishMenu;
using DBT.UserInterfaces.HairMenu;
using DBT.Items.Tiles.MusicBoxes;
using DBT.Tiles.MusicBoxes;
using DBT.UserInterfaces.TechniqueMenu;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using WebmilioCommons.Networking;

namespace DBT
{
    public sealed class DBTMod : Mod
    {
        internal ModHotKey characterMenuKey, energyChargeKey, transformDownKey, speedToggleKey, transformUpKey, flightToggleKey, instantTransmission, techniqueMenuKey;

        internal KiBar kiBar;
        internal UserInterface kiBarInterface;

        internal static CircleShader circle;
        internal UserInterface kiBrowserInterface;
        internal OverloadBar overloadBar;
        internal UserInterface overloadBarInterface;
        internal DBTMenu dbtMenu;
        internal CharacterTransformationsMenu characterTransformationsMenu;
        internal UserInterface characterMenuInterface;
        internal TechniqueMenu techniqueMenu;
        internal UserInterface techniqueMenuInterface;
        internal WishMenu wishMenu;
        internal UserInterface wishMenuInterface;
        internal HairMenu hairMenu;
        internal UserInterface hairMenuInterface;
        internal NamekianBookUI namekBookUI;
        internal UserInterface namekBookInterface;

        internal bool calamityEnabled;

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
                HairGFX.LoadHairGFX(this);
                WishMenuGFX.LoadWishGFX(this);

                SteamHelper.Initialize();

                #region HotKeys

                characterMenuKey = RegisterHotKey("Character Menu", "K");
                energyChargeKey = RegisterHotKey("Energy Charge", "C");
                speedToggleKey = RegisterHotKey("Speed Toggle", "Z");
                transformDownKey = RegisterHotKey("Transform Down", "V");
                transformUpKey = RegisterHotKey("Transform Up", "X");
                flightToggleKey = RegisterHotKey("Flight Toggle", "Q");
                instantTransmission = RegisterHotKey("Instant Transmission", "I");
                techniqueMenuKey = RegisterHotKey("Technique Menu", "N");

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

                characterTransformationsMenu = new CharacterTransformationsMenu(this);
                characterTransformationsMenu.Activate();
                characterMenuInterface = new UserInterface();
                characterMenuInterface.SetState(characterTransformationsMenu);

                techniqueMenu = new TechniqueMenu(this);
                techniqueMenu.Activate();
                techniqueMenuInterface = new UserInterface();
                techniqueMenuInterface.SetState(techniqueMenu);

                wishMenu = new WishMenu();
                wishMenu.Activate();
                wishMenuInterface = new UserInterface();
                wishMenuInterface.SetState(wishMenu);

                hairMenu = new HairMenu();
                hairMenu.Activate();
                hairMenuInterface = new UserInterface();
                hairMenuInterface.SetState(hairMenu);

                namekBookUI = new NamekianBookUI();
                namekBookUI.Activate();
                namekBookInterface = new UserInterface();
                namekBookInterface.SetState(namekBookUI);

                Ref<Effect> screenRef = new Ref<Effect>(GetEffect("Effects/ShockwaveEffect"));

                Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
                Filters.Scene["Shockwave"].Load();

                circle = new CircleShader(new Ref<Effect>(GetEffect("Effects/CircleShader")), "Pass1");

                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MusicBoxes/BirthOfAGod"), ModContent.ItemType<AngelStaffBoxItem>(), ModContent.TileType<AngelStaffBoxTile>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MusicBoxes/HeadChala"), ModContent.ItemType<OneStarBoxItem>(), ModContent.TileType<OneStarBoxTile>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MusicBoxes/Budokai2"), ModContent.ItemType<TwoStarBoxItem>(), ModContent.TileType<TwoStarBoxTile>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MusicBoxes/Budokai3"), ModContent.ItemType<ThreeStarBoxItem>(), ModContent.TileType<ThreeStarBoxTile>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MusicBoxes/SSJ3Song"), ModContent.ItemType<FourStarBoxItem>(), ModContent.TileType<FourStarBoxTile>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MusicBoxes/Challenge"), ModContent.ItemType<FiveStarBoxItem>(), ModContent.TileType<FiveStarBoxTile>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MusicBoxes/LostCourage"), ModContent.ItemType<SixStarBoxItem>(), ModContent.TileType<SixStarBoxTile>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MusicBoxes/VegetaSSJ"), ModContent.ItemType<SevenStarBoxItem>(), ModContent.TileType<SevenStarBoxTile>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MusicBoxes/XV2Villain"), ModContent.ItemType<DragonBallsBoxItem>(), ModContent.TileType<DragonBallsBoxTile>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MusicBoxes/BurstLimit"), ModContent.ItemType<BabidisMagicBoxItem>(), ModContent.TileType<BabidisMagicBoxTile>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/TheBarrenWastelands"), ModContent.ItemType<WastelandsBoxItem>(), ModContent.TileType<WastelandsBoxTile>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/TheUnexpectedArrival"), ModContent.ItemType<FFBoxItem>(), ModContent.TileType<FFBoxTile>());
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                HairGFX.UnloadHairGFX();
                WishMenuGFX.UnloadWishGFX();

                kiBar.Visible = false;

                characterTransformationsMenu.Visible = false;

                techniqueMenu.Visible = false;

                overloadBar.Visible = false;

                WishMenu.menuVisible = false;

                HairMenu.menuVisible = false;

                namekBookUI.MenuVisible = false;
            }
            Instance = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (characterMenuInterface != null && characterTransformationsMenu.Visible)
                characterMenuInterface.Update(gameTime);

            if (techniqueMenuInterface != null && techniqueMenu.Visible)
                techniqueMenuInterface.Update(gameTime);

            if (wishMenuInterface != null && WishMenu.menuVisible)
                wishMenuInterface.Update(gameTime);

            if (hairMenuInterface != null && HairMenu.menuVisible)
                hairMenuInterface.Update(gameTime);

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
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/TheBarrenWastelands");
            }
            if (player.active && player.GetModPlayer<DBTPlayer>(this).zoneUGWasteland && !Main.gameMenu && playMusic)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/TheInfestedNest");
            }
        }
        public override void PostSetupContent()
        {
            // Boss checklist support
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddBossWithInfo", "A Frieza Force Ship", 3.8f, (Func<bool>)(() => Worlds.DBTWorld.downedFriezaShip), "Alert and let a frieza force scout escape in the wasteland biome after the world evil has been killed.");
            }

            calamityEnabled = ModLoader.GetMod("CalamityMod") != null;
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
                layers.Insert(resourcesLayerIndex, new WishMenuLayer());
                layers.Insert(resourcesLayerIndex, new HairMenuLayer());
                layers.Insert(resourcesLayerIndex, new NamekianBookUILayer());
            }
            if (characterMenuIndex != -1)
            {
                layers.Insert(characterMenuIndex, new CharacterTransformationsMenuLayer(characterTransformationsMenu, characterMenuInterface));
                layers.Insert(characterMenuIndex, new TechniqueMenuLayer(techniqueMenu, techniqueMenuInterface));
            }
        }

        public override void HandlePacket(BinaryReader binaryReader, int whoAmI)
        {
            NetworkPacketLoader.Instance.HandlePacket(binaryReader, whoAmI);
        }
    
        public static uint GetTicks() => Main.GameUpdateCount;
        public static bool IsTickRateElapsed(int rateModulo) => GetTicks() > 0 && GetTicks() % rateModulo == 0;
        public static DBTMod Instance { get; set; }
    }
}
