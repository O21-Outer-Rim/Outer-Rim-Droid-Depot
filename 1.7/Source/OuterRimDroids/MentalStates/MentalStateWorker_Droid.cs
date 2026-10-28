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
    public class MentalStateWorker_Droid : MentalStateWorker
    {
        public override bool StateCanOccur(Pawn pawn)
        {
            if (base.StateCanOccur(pawn))
            {
                return false;
            }
            if (!pawn.def.HasModExtension<DefModExt_Droid>())
            {
                return false;
            }
            return true;
        }
    }
}
