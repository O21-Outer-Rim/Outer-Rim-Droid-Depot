using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

using OuterRimDroids;
using HarmonyLib;

namespace OuterRimDroids_Ideology
{
	[HarmonyPatch(typeof(TaleUtility), "Notify_PawnDied")]
	public static class Patch_TaleUtility_Notify_PawnDied
	{
		[HarmonyPostfix]
		private static void Postfix(Pawn victim, DamageInfo? dinfo)
		{
			if (victim != null)
			{
				if (victim.def.HasModExtension<DefModExt_Droid>())
				{
					Pawn pawn = ((dinfo != null) ? dinfo.GetValueOrDefault().Instigator : null) as Pawn;
					if (pawn != null)
					{
						Find.HistoryEventsManager.RecordEvent(new HistoryEvent(OuterRimDroidsIdeoDefOf.OuterRim_DroidDestroyed, new SignalArgs(pawn.Named(HistoryEventArgsNames.Doer))), true);
					}
				}
			}
		}
	}
}
