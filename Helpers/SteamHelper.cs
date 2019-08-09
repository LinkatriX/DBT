using DBT.Commons.Users;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader;

// ReSharper disable IdentifierTypo
namespace DBT.Helpers
{
    public static class SteamHelper
    {
        private static bool _initialized = false;

        private static readonly List<Developer> _activeDevelopers = new List<Developer>()
        {
            FullNovaAlchemist, NuovaPrime, Webmilio
        };

        private static readonly List<Donator> _activeDonators = new List<Donator>()
        {
            BrushBoy,
            CanadianMRE,
            DaddyWonka,
            FreeRaisinBread,
            Hamster, HelloMyFriend,
            Luna,
            Mak0, Maxphog, Megawarrior101,
            NyoonBooetteEnthusiast,
            PapaTingle, Pheonix,
            Outnot,
            Redflare,
            Skipping, SnorLaxatives, SoulCarnagee, StepDad,
            UndeadDeath,
            Vector,
            William
        };

        public static void Initialize()
        {
            if (_initialized) return;

            try
            {
                string unparsedSteamID64 = typeof(ModLoader).GetProperty("SteamID64", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null).ToString();

                if (!string.IsNullOrWhiteSpace(unparsedSteamID64))
                {
                    HasSteamId64 = true;
                    SteamId64 = long.Parse(unparsedSteamID64);
                }
            }
            catch (Exception)
            {
                //Console.WriteLine("Unable to fetch SteamID, assuming no steam is present.");
            }

            if (!HasSteamId64) return;

            foreach (Donator donator in _activeDonators)
                if (donator.SteamId64 == SteamId64)
                {
                    CurrentUser = donator;

                    donator.IsCurrentUser = true;

                    CurrentDonator = donator;
                    IsDonator = true;

                    break;
                }

            foreach (Developer developer in _activeDevelopers)
                if (developer.SteamId64 == SteamId64)
                {
                    CurrentUser = developer;

                    developer.IsCurrentUser = true;

                    CurrentDeveloper = developer;
                    IsDeveloper = true;

                    break;
                }
        }

        public static bool CanAccess() => CurrentUser != null;
        public static bool CanAccess(User user) => CanAccess(user, false);

        public static bool CanAccess(User user, bool ignoreDevelopers) => !ignoreDevelopers && IsDeveloper || CanAccess(user);

        public static bool CanAccess(bool ignoreDevelopers, bool donators) => ignoreDevelopers && IsDeveloper || donators && IsDonator;

        public static bool CanAccess(params User[] users) => CanAccess(false, users);

        public static bool CanAccess(bool ignoreDevelopers, params User[] users)
        {
            if (!ignoreDevelopers && IsDeveloper) return true;

            for (int i = 0; i < users.Length; i++)
                if (users[i] == CurrentUser)
                    return true;

            return false;
        }


        public static bool HasSteamId64 { get; private set; }
        public static long SteamId64 { get; private set; }

        public static User CurrentUser { get; private set; }

        public static Developer CurrentDeveloper { get; private set; }
        public static bool IsDeveloper { get; private set; }

        public static Donator CurrentDonator { get; private set; }
        public static bool IsDonator { get; private set; }

        #region Developers

        public static Developer Webmilio => new Developer(76561198046878487, "webmilio", 247893661990387713);
        public static Developer NuovaPrime => new Developer(76561198323188001, "NuovaPrime", 188696025085509634);
        public static Developer FullNovaAlchemist => new Developer(76561198128593049, "FullNovaAlchemist", 425384277434302474);

        #endregion


        #region Donators

        public static Donator BrushBoy => new Donator(76561197960287930, "Brush Boy", 200413799746895873); // Brush Boy#9064
        public static Donator CanadianMRE => new Donator(76561198147284656, "CanadianMRE", 219196190271471616); // CanadianMRE#6288
        public static Donator DaddyWonka => new Donator(76561198256554091, "DaddyWonka", 267171454234132480); // DaddyWonka#8546
        public static Donator FreeRaisinBread => new Donator(368799789942308874, "FreeRasinBread", 368799789942308874); // Freerasinbread#3516
        public static Donator Hamster => new Donator(76561198055274667, "Hamster", 224323430651133952); // Hamster#2477
        public static Donator HelloMyFriend => new Donator(76561198062878746, "Hello My Friend", 251408085313126401); // hello my friend#2456
        public static Donator Luna => new Donator(76561198085728391, "Luna", 216853147837005824); // [MU] Luna#8888
        public static Donator Mak0 => new Donator(76561198161525177, "Михаил", 307618173073489920); // Mak0-Z#6790
        public static Donator Maxphog => new Donator(765661198278045015, "Maxphog", 256982543025307648); // maxphog#3903
        public static Donator Megawarrior101 => new Donator(76561193783431419, "Megawarrior_101", 405844470584836117); // Megawarrior_101#0616
        public static Donator NyoonBooetteEnthusiast => new Donator(76561198301260251, "Nyoon Booette Enthusiast", 228294779815985152); // Nyoon Booette enthusiast#4130;
        public static Donator Outnot => new Donator(76561198118731558, "Outnot", 265734710561472512); // Outnot#1305
        public static Donator PapaTingle => new Donator(76561198236967076, "Papa Tingle", 159089770763517953); // PapaTingle#5281
        public static Donator Pheonix => new Donator(76561198059019480, "Pheonix", 175445960946745344); // pheonix#7035
        public static Donator Redflare => new Donator(76561197973020859, "Redflare", 310461222115868673); // Redflare#5473
        public static Donator Skipping => new Donator(76561193979609866, "Luke", 450018452103757835); // Skipping#7613
        public static Donator SnorLaxatives => new Donator(76561198108193073, "SnorLaxatives", 189983407168684042); // SnorLaxatives#2131
        public static Donator SoulCarnagee => new Donator(76561198059100989, "SoulCarnagee", 313358705854775296); // SoulCarnagee#6993
        public static Donator StepDad => new Donator(76561197992913271, "Step-Dad", 164133212560293888); // Folv#1251
        public static Donator UndeadDeath => new Donator(76561198168677946, "UndeadDeath", 250889704222883841); // UndeadDeath#0768
        public static Donator Vector => new Donator(76561198248632856, "Vector", 300040297256058881); // Mr.Bombastic#1706
        public static Donator William => new Donator(76561198090096918, "William", 141746262490873857); // william#0117

        #endregion
    }
}
