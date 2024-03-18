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

namespace OuterRimDroids_Ideology
{
    public class OuterRimDroidsIdeoMod : Mod
    {
        public static OuterRimDroidsIdeoMod mod;
        public static OuterRimDroidsIdeoSettings settings;

        public OuterRimDroidsIdeoMod(ModContentPack content) : base(content)
        {
            mod = this;
            settings = GetSettings<OuterRimDroidsIdeoSettings>();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;

            LogUtil.LogMessage($"Ideology Compatability Enabled ::");

            Harmony OuterRimHarmony = new Harmony("Neronix17.OuterRimDroids_Ideology.RimWorld");
            OuterRimHarmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        //public override string SettingsCategory() => "Outer Rim - Droids (Ideology)";

        //public override void DoSettingsWindowContents(Rect inRect)
        //{
        //    base.DoSettingsWindowContents(inRect);
        //}
    }
}
