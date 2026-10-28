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
	[HarmonyPatch]
	public static class Patch_EquipmentUtility_CanEquip
    {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod() => AccessTools.Method(typeof(EquipmentUtility), nameof(EquipmentUtility.CanEquip), new[] { typeof(Thing), typeof(Pawn), typeof(string).MakeByRefType(), typeof(bool) });

        [HarmonyPostfix]
        public static void Postfix(ref bool __result, Thing thing, Pawn pawn, ref string cantReason)
        {
            if (__result)
            {
                bool thingIsDroidStuff = thing.def.HasModExtension<DefModExt_DroidEquipment>();
                bool pawnIsDroid = pawn.def.HasModExtension<DefModExt_DroidEquipment>();
                if (thingIsDroidStuff && !pawnIsDroid)
                {
                    __result = false;
                    cantReason = "OuterRimDroids.CantEquipReasonNotDroid".Translate();
                    return;
                }
                if (thing.def.IsApparel && !OuterRimDroidsMod.settings.droidApparelCheck)
                {
                    if (pawnIsDroid && !thingIsDroidStuff)
                    {
                        __result = false;
                        cantReason = "OuterRimDroids.DroidsCannotUse".Translate();
                        return;
                    }
                }
            }
        }
    }
}
