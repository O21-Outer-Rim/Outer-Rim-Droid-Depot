using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace OuterRimDroids
{
    public class Comp_DroidHealer : ThingComp
	{
		public CompProperties_DroidHealer Props => (CompProperties_DroidHealer)props;

		public int tickCounter = 0;

		public List<Pawn> pawnList = new List<Pawn>();

		public Pawn thisPawn;

		public override void CompTick()
		{
			base.CompTick();
			tickCounter++;
			if (tickCounter <= Props.tickInterval)
			{
				return;
			}
			thisPawn = parent as Pawn;
			if (thisPawn != null && thisPawn.Map != null && !thisPawn.Dead && !thisPawn.Downed && thisPawn.Faction != null)
			{
				foreach (Thing item in GenRadial.RadialDistinctThingsAround(thisPawn.Position, thisPawn.Map, Props.radius, useCenter: true))
				{
					if (!(item is Pawn pawn) || pawn.Dead || pawn == parent || thisPawn.Faction != pawn.Faction || !pawn.def.HasModExtension<DefModExt_Droid>())
					{
						continue;
					}
					if (pawn.health == null)
					{
						continue;
					}
					IEnumerable<Hediff> injuriesTendable = pawn.health.hediffSet.GetHediffsTendable();
					if (injuriesTendable == null)
					{
						continue;
					}
					Hediff[] array = injuriesTendable.ToArray();
					if (!array.Any())
					{
						continue;
					}
					array.RandomElement().Heal(Props.healAmount);
					{
						// TODO: Replace with a sound and visual more appropriate for droid healing.
						//SoundDefOf.PsychicPulseGlobal.PlayOneShot(new TargetInfo(parent.Position, parent.Map));
						//FleckMaker.AttachedOverlay(parent, DefDatabase<FleckDef>.GetNamed("PsycastPsychicEffect"), Vector3.zero);
					}
				}
			}
			tickCounter = 0;
		}
	}
}
