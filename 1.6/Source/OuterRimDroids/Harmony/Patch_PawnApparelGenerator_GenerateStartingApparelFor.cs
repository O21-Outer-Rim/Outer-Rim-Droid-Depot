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

namespace OuterRimDroids
{
    [HarmonyPatch(typeof(PawnApparelGenerator), "GenerateStartingApparelFor")]
    public static class Patch_PawnApparelGenerator_GenerateStartingApparelFor
    {
        [HarmonyPrefix]
        public static void Prefix(Pawn pawn)
        {
            ApparelUtil.apparelList = new HashSet<ThingStuffPair>();
            foreach (ThingStuffPair item in CachedUtil.allApparelPairs().ListFullCopy())
            {
                if (!ApparelUtil.CanWear(item.thing, pawn.def))
                {
                    ApparelUtil.apparelList.Add(item);
                }
            }
            CachedUtil.allApparelPairs().RemoveAll((ThingStuffPair tsp) => ApparelUtil.apparelList.Contains(tsp));
        }

        [HarmonyPostfix]
        public static void Postfix()
        {
            CachedUtil.allApparelPairs().AddRange(ApparelUtil.apparelList);
        }
    }
}
