using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

            LogUtil.LogMessage($"{CurrentVersion} ::");

            if (Prefs.DevMode)
            {
                File.WriteAllText(VersionDir, CurrentVersion);
            }

            harmony = new Harmony("Neronix17.OuterRimDroids.RimWorld");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override string SettingsCategory() => "Outer Rim - Droid Depot";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);
            listing.CheckboxLabeled("Droids can use non-droid apparel", ref settings.droidApparelCheck, "If enabled (disabled by default), allows droids to wear non-droid apparel.");
            listing.End();
        }
    }
}
