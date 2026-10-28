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

namespace OuterRimDroids
{
    [HarmonyPatch(typeof(Thing), "ButcherProducts")]
    static class Patch_Thing_ButcherProducts
    {

        [HarmonyPostfix]
        static void Postfix(Thing __instance, ref IEnumerable<Thing> __result, float efficiency)
        {
            if (__instance is Pawn && ((Pawn)__instance).def.HasModExtension<DefModExt_Droid>())
            {
                Pawn pawn = __instance as Pawn;
                __result = GenerateExtraProducts(__result, pawn, efficiency);
            }
        }

        private static IEnumerable<Thing> GenerateExtraProducts(IEnumerable<Thing> things, Pawn pawn, float efficiency)
        {
            foreach (Thing thing in things)
            {
                yield return thing;
            }

            if (pawn.health.hediffSet.GetNotMissingParts().Any(bpr => bpr.def == OuterRimDroidsDefOf.OuterRim_DroidBrain))
            {
                Thing brain = ThingMaker.MakeThing(OuterRimDroidsThingDefOf.OuterRim_DroidBrain, null);
                yield return brain;
            }

            yield break;
        }
    }
}
