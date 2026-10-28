using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace OuterRimDroids
{
    public class Recipe_RemoveBolt : Recipe_RemoveImplant
    {
        public override bool AvailableOnNow(Thing thing, BodyPartRecord bodyPartRecord = null)
        {
            Pawn pawn = thing as Pawn;
            if (!pawn?.health?.hediffSet?.HasHediff(OuterRimDroidsDefOf.OuterRim_RestraintBolt) ?? false)
            {
                return false;
            }

            return base.AvailableOnNow(thing);
        }

        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            base.ApplyOnPawn(pawn, part, billDoer, ingredients, bill);
            if (pawn?.health?.hediffSet?.HasHediff(OuterRimDroidsDefOf.OuterRim_RestraintBolt) ?? false)
            {
                pawn.health.hediffSet.hediffs.Remove(
                    pawn.health.hediffSet.GetFirstHediffOfDef(OuterRimDroidsDefOf.OuterRim_RestraintBolt));
            }
        }
    }
}
