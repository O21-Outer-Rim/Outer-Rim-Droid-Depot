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
	[HarmonyPatch(typeof(MentalBreakWorker), "BreakCanOccur")]
	public static class Patch_MentalBreakWorker_BreakCanOccur
	{
		[HarmonyPrefix]
		public static bool Prefix(MentalBreakWorker __instance, bool __result, Pawn pawn)
		{
			if (pawn.health.hediffSet.HasHediff(OuterRimDroidsDefOf.OuterRim_RestraintBolt) || pawn.story.traits.HasTrait(OuterRimDroidsDefOf.OuterRim_SelfRestrained))
			{
				__result = false;
				return false;
			}
			return true;
		}
	}
}
