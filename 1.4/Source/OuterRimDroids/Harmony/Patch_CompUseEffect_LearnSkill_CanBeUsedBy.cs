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
    [HarmonyPatch(typeof(CompUseEffect_LearnSkill), "CanBeUsedBy")]
    public static class Patch_CompUseEffect_LearnSkill_CanBeUsedBy
    {
        [HarmonyPostfix]
        public static void Postfix(ref bool __result, Pawn p, string failReason)
        {
            if (p.def.HasModExtension<DefModExt_Droid>())
            {
                failReason = "OuterRimDroids.DroidsCannotUse";
                __result = false;
                return;
            }
        }
    }
}
