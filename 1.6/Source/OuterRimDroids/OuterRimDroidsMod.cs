using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TabulaRasa;
using UnityEngine;
using Verse;

namespace OuterRimDroids
{
    public class OuterRimDroidsMod : Mod
    {
        public static OuterRimDroidsMod mod;
        public static OuterRimDroidsSettings settings;
        public static Harmony harmony;

        internal static string VersionDir => Path.Combine(mod.Content.ModMetaData.RootDir.FullName, "Version.txt");
        public static string CurrentVersion { get; private set; }

        public OuterRimDroidsMod(ModContentPack content) : base(content)
        {
            mod = this;
            settings = GetSettings<OuterRimDroidsSettings>();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            CurrentVersion = $"{version.Major}.{version.Minor}.{version.Build}";

            Log.Message($":: Outer Rim - Droid Depot :: ".Colorize(Color.cyan) + $"{CurrentVersion} ::");

            if (Prefs.DevMode)
            {
                File.WriteAllText(VersionDir, CurrentVersion);
            }

            harmony = new Harmony("Neronix17.OuterRimDroids.RimWorld");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void DoOptionsCategoryContents(Listing_Standard listing)
        {
            listing.GapLine();
            listing.Note("Droid Depot", GameFont.Medium);
            listing.GapLine();
            listing.CheckboxLabeled("Droids can use non-droid apparel", ref settings.droidApparelCheck, "If enabled (disabled by default), allows droids to wear non-droid apparel.");
        }
    }
}
