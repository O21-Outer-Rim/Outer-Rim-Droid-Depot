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
    public class Comp_TargetableOnDownedDroid : CompTargetable
    {
        public override bool PlayerChoosesTarget
        {
            get
            {
                return true;
            }
        }

        public override TargetingParameters GetTargetingParameters()
        {
            return new TargetingParameters
            {
                canTargetPawns = true,
                canTargetBuildings = false,
                canTargetItems = false,
                mapObjectTargetsMustBeAutoAttackable = false,
                validator = (TargetInfo x) => (x.Thing is Corpse && TargetValidatorCorpse((Corpse)x.Thing)) || x.Thing is Pawn && TargetValidatorPawn((Pawn)x.Thing)
            };
        }

        public override IEnumerable<Thing> GetTargets(Thing targetChosenByPlayer = null)
        {
            yield return targetChosenByPlayer;
            yield break;
        }

        public bool TargetValidatorCorpse(Corpse t)
        {
            Pawn pawn = t?.InnerPawn ?? null;
            if (pawn != null && pawn.def.HasModExtension<DefModExt_Droid>())
            {
                if (t != null && pawn.health.hediffSet.GetBrain() != null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool TargetValidatorPawn(Pawn pawn)
        {
            if (pawn != null && pawn.def.HasModExtension<DefModExt_Droid>())
            {
                if (pawn.Downed || pawn.IsPrisoner)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
