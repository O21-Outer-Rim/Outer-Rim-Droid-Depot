using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace OuterRimDroids
{
    [DefOf]
    public static class OuterRimDroidsDefOf
    {
        static OuterRimDroidsDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(OuterRimDroidsDefOf));
        }

        // Droids
        public static HediffDef OuterRim_RestraintBolt;
        public static HediffDef OuterRim_DroidArm_Makeshift;
        public static HediffDef OuterRim_DroidLeg_Makeshift;

        public static BodyPartDef OuterRim_DroidArm;
        public static BodyPartDef OuterRim_DroidLeg;
        public static BodyPartDef OuterRim_DroidBrain;

        public static TraitDef OuterRim_Rebellious;
        public static TraitDef OuterRim_Crosswired;
        public static TraitDef OuterRim_Rustbrained;
        public static TraitDef OuterRim_Choppy;
        public static TraitDef OuterRim_LooseScrews;
        public static TraitDef OuterRim_Speedy;
        public static TraitDef OuterRim_SelfRestrained;
        public static TraitDef OuterRim_Twitchy;
        public static TraitDef OuterRim_Twitterer;

        public static ThingCategoryDef OuterRim_SkillDatadisks;

        //public static RecipeDef OuterRim_ButcherCorpseDroid;

        public static JobDef OuterRim_ReactivateDroid;
        public static JobDef OuterRim_ReprogramDroid;
        public static JobDef OuterRim_RestrainDroid;
        public static JobDef OuterRim_UseSkillDatadisk;
    }

    [DefOf]
    public static class OuterRimDroidsThingDefOf
    {
        static OuterRimDroidsThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(OuterRimDroidsThingDefOf));
        }

        public static ThingDef OuterRim_DroidBrain;
    }
}
