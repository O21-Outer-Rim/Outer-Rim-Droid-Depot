using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace OuterRimDroids_Ideology
{
    [DefOf]
    public static class OuterRimDroidsIdeoDefOf
    {
        static OuterRimDroidsIdeoDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(OuterRimDroidsIdeoDefOf));
        }

        public static MemeDef OuterRim_DroidPrimacy;
        public static PreceptDef OuterRim_Droids_Loved;
        public static PreceptDef OuterRim_Droids_Hated;
        public static HistoryEventDef OuterRim_DroidDestroyed;
    }
}
