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
using Asimov;

namespace OuterRimDroids
{
	[HarmonyPatch(typeof(FloatMenuMakerMap), "ChoicesAtFor")]
	public static class Patch_FloatMenuMakerMap_ChoicesAtFor
	{
		[HarmonyPostfix]
		public static void Postfix(ref List<FloatMenuOption> __result, Vector3 clickPos, Pawn pawn, bool suppressAutoTakeableGoto = false)
		{
			if (pawn.IsAutomaton() && pawn.def.HasModExtension<DefModExt_DroidEquipment>())
			{
				IntVec3 c = IntVec3.FromVector3(clickPos);
				c.GetThingList(pawn.Map);
				Apparel ap = pawn.Map.thingGrid.ThingAt<Apparel>(c);
				if (ap != null)
				{
					if (ApparelUtil.CanWear(ap.def, pawn.def))
					{
						string cantReason;
						FloatMenuOption op = EquipmentUtility.CanEquip(ap, pawn, out cantReason) ? FloatMenuUtility.DecoratePrioritizedTask(new FloatMenuOption("ForceWear".Translate(ap.LabelShort, ap), delegate
						{
							ap.SetForbidden(value: false);
							Job job = JobMaker.MakeJob(JobDefOf.Wear, ap);
							pawn.jobs.TryTakeOrderedJob(job, JobTag.Misc);
						}, MenuOptionPriority.High), pawn, ap) : new FloatMenuOption("CannotWear".Translate(ap.Label, ap) + ": " + cantReason, null);
						__result.Add(op);
					}
				}
			}
            else
			{
				LogUtil.LogMessage("Should NOT Be Listed!");
			}
		}
	}
}
