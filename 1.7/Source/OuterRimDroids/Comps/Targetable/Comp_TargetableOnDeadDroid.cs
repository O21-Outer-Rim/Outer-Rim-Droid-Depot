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
    public class Comp_TargetableOnDeadDroid : CompTargetable
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
                canTargetBuildings = true,
                canTargetItems = true,
                mapObjectTargetsMustBeAutoAttackable = false,
                validator = ((TargetInfo x) => x.Thing is Corpse && TargetValidator((Corpse)x.Thing))
            };
        }

        public override IEnumerable<Thing> GetTargets(Thing targetChosenByPlayer = null)
        {
            yield return targetChosenByPlayer;
            yield break;
        }

        public bool TargetValidator(Corpse t)
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

    }
}
