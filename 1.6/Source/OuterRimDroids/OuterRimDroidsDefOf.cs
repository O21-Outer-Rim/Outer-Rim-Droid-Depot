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
        public static HediffDef 
            OuterRim_RestraintBolt,
            OuterRim_DroidArm_Makeshift,
            OuterRim_DroidLeg_Makeshift;

        public static BodyPartDef 
            OuterRim_DroidArm,
            OuterRim_DroidLeg,
            OuterRim_DroidBrain;

        public static TraitDef 
            OuterRim_Rebellious,
            OuterRim_Crosswired,
            OuterRim_Rustbrained,
            OuterRim_Choppy,
            OuterRim_LooseScrews,
            OuterRim_Speedy,
            OuterRim_SelfRestrained,
            OuterRim_Twitchy,
            OuterRim_Twitterer;

        public static ThingCategoryDef 
            OuterRim_SkillDatadisks;

        //public static RecipeDef OuterRim_ButcherCorpseDroid;

        public static JobDef 
            OuterRim_ReactivateDroid,
            OuterRim_ReprogramDroid,
            OuterRim_RestrainDroid,
            OuterRim_UseSkillDatadisk;
    }

    [DefOf]
    public static class OuterRimDroidsThingDefOf
    {
        static OuterRimDroidsThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(OuterRimDroidsThingDefOf));
        }

        public static ThingDef 
            OuterRim_DroidBrain;
    }
}
