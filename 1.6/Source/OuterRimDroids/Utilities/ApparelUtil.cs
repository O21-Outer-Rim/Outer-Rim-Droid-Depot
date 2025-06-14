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
    public static class ApparelUtil
    {
        public static HashSet<ThingStuffPair> apparelList;

        public static bool CanWear(ThingDef thing, ThingDef pawn)
        {
            bool thingIsDroidStuff = thing.HasModExtension<DefModExt_DroidEquipment>();
            bool pawnIsDroid = pawn.HasModExtension<DefModExt_DroidEquipment>();
            if (thingIsDroidStuff && !pawnIsDroid)
            {
                return false;
            }
            if (!OuterRimDroidsMod.settings.droidApparelCheck && !thingIsDroidStuff && pawnIsDroid)
            {
                return false;
            }
            return true;
        }
    }
}
