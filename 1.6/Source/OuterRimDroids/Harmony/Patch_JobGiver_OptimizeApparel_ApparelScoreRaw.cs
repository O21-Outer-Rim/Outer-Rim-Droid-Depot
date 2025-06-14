using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

using HarmonyLib;
using Verse.AI;
using System.Reflection;

namespace OuterRimDroids
{
    [HarmonyPatch(typeof(JobGiver_OptimizeApparel), "ApparelScoreRaw")]
    public static class Patch_JobGiver_OptimizeApparel_ApparelScoreRaw
    {
        [HarmonyPostfix]
        public static void Postfix(ref float __result, Pawn pawn, Apparel ap)
        {
            if (__result > 0f)
            {
                if (!ApparelUtil.CanWear(ap.def, pawn.def))
                {
                    __result = -1000f;
                    return;
                }
            }
        }
    }
}
