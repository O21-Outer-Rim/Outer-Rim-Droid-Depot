using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.AI;

namespace OuterRimDroids
{
	public class JobDriver_ReactivateDroid : JobDriver
	{
		private const TargetIndex CorpseInd = TargetIndex.A;

		private const TargetIndex ItemInd = TargetIndex.B;

		private const int DurationTicks = 600;

		private Thing Item
		{
			get
			{
				return this.job.GetTarget(TargetIndex.B).Thing;
			}
		}
		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return this.pawn.Reserve(job.GetTarget(TargetIndex.A).Thing as Corpse != null ? job.GetTarget(TargetIndex.A).Thing as Corpse : job.GetTarget(TargetIndex.A).Thing as Pawn, this.job, 1, -1, null, errorOnFailed) && this.pawn.Reserve(this.Item, this.job, 1, -1, null, errorOnFailed);
		}
		public override IEnumerable<Toil> MakeNewToils()
		{
			yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.B).FailOnDespawnedOrNull(TargetIndex.A);
			yield return Toils_Haul.StartCarryThing(TargetIndex.B, false, false, false);
			yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.A);
			Toil toil = Toils_General.Wait(600, TargetIndex.None);
			toil.WithProgressBarToilDelay(TargetIndex.A, false, -0.5f);
			toil.FailOnDespawnedOrNull(TargetIndex.A);
			toil.FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch);
			yield return toil;
			yield return Toils_General.Do(job.GetTarget(TargetIndex.A).Thing as Corpse != null ? new Action(Resurrect) : new Action(Restore));
			yield break;
		}

		public void Restore()
        {
			Pawn pawn = job.GetTarget(TargetIndex.A).Thing as Pawn;
			RepairWithSideEffects(pawn);
			Messages.Message(pawn.Name + " was successfully restored!", pawn, MessageTypeDefOf.PositiveEvent, true);
			Item.SplitOff(1).Destroy(DestroyMode.Vanish);
		}

		public static void RepairWithSideEffects(Pawn pawn)
		{
			float partsLeft;
			if (pawn != null)
			{
				partsLeft = 1f - pawn.health.hediffSet.GetCoverageOfNotMissingNaturalParts(pawn.RaceProps.body.corePart);
			}
			else
			{
				partsLeft = 0f;
			}
			IEnumerable<Hediff_Injury> injuriesTendable = pawn.health.hediffSet.GetInjuriesTendable();
			if (injuriesTendable != null)
			{
				Hediff_Injury[] array = injuriesTendable.ToArray();
				if (array.Any())
				{
                    for (int i = 0; i < array.Length; i++)
                    {
						array[i].Heal(100f);
                    }
				}
			}
			if (Rand.Chance(partsLeft))
			{
				foreach (BodyPartRecord bpr in from x in pawn.health.hediffSet.GetNotMissingParts() where x.def == OuterRimDroidsDefOf.OuterRim_DroidArm select x)
				{
					if (!pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(bpr))
					{
						Hediff hediff = HediffMaker.MakeHediff(OuterRimDroidsDefOf.OuterRim_DroidArm_Makeshift, pawn, bpr);
						pawn.health.AddHediff(hediff);
					}
				}
			}
			if (Rand.Chance(partsLeft))
			{
				foreach (BodyPartRecord bpr in from x in pawn.health.hediffSet.GetNotMissingParts() where x.def == OuterRimDroidsDefOf.OuterRim_DroidLeg select x)
				{
					if (!pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(bpr))
					{
						Hediff hediff = HediffMaker.MakeHediff(OuterRimDroidsDefOf.OuterRim_DroidLeg_Makeshift, pawn, bpr);
						pawn.health.AddHediff(hediff);
					}
				}
			}
			List<Hediff> missingParts = new List<Hediff>();
			pawn.health.hediffSet.GetHediffs(ref missingParts, h => h.def == HediffDefOf.MissingBodyPart);
            for (int i = 0; i < missingParts.Count; i++)
            {
				pawn.health.RemoveHediff(missingParts[i]);
            }
			if (pawn.Dead)
			{
				LogUtil.LogError("The pawn has died while being repaired, resurrecting. Shouldn't happen but you never know with this game.");
				ResurrectionUtility.Resurrect(pawn);
			}
		}

		public void Resurrect()
		{
			Pawn innerPawn = (job.GetTarget(TargetIndex.A).Thing as Corpse).InnerPawn;
			ReactivateWithSideEffects(innerPawn);
			Messages.Message(innerPawn.Name + " was successfully reactivated!", innerPawn, MessageTypeDefOf.PositiveEvent, true);
			Item.SplitOff(1).Destroy(DestroyMode.Vanish);
		}

		public static void ReactivateWithSideEffects(Pawn pawn)
		{
			Corpse corpse = pawn.Corpse;
			float partsLeft;
			if (corpse != null)
			{
				partsLeft = 1f - corpse.InnerPawn.health.hediffSet.GetCoverageOfNotMissingNaturalParts(corpse.InnerPawn.RaceProps.body.corePart);
			}
			else
			{
				partsLeft = 0f;
			}
			ResurrectionUtility.Resurrect(pawn);
			BodyPartRecord brain = pawn.health.hediffSet.GetBrain();
			if (Rand.Chance(partsLeft))
			{
				foreach (BodyPartRecord bpr in from x in pawn.health.hediffSet.GetNotMissingParts() where x.def == OuterRimDroidsDefOf.OuterRim_DroidArm select x)
				{
					if (!pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(bpr))
					{
						Hediff hediff = HediffMaker.MakeHediff(OuterRimDroidsDefOf.OuterRim_DroidArm_Makeshift, pawn, bpr);
						pawn.health.AddHediff(hediff);
					}
				}
			}
			if (Rand.Chance(partsLeft))
			{
				foreach (BodyPartRecord bpr in from x in pawn.health.hediffSet.GetNotMissingParts() where x.def == OuterRimDroidsDefOf.OuterRim_DroidLeg select x)
				{
					if (!pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(bpr))
					{
						Hediff hediff = HediffMaker.MakeHediff(OuterRimDroidsDefOf.OuterRim_DroidLeg_Makeshift, pawn, bpr);
						pawn.health.AddHediff(hediff);
					}
				}
			}
			if (pawn.Dead)
			{
				LogUtil.LogError("The pawn has died while being resurrected.");
				ResurrectionUtility.Resurrect(pawn);
			}
		}
	}
}
