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
    public class CompProperties_DroidHealer : CompProperties
    {
        public CompProperties_DroidHealer()
        {
            compClass = typeof(Comp_DroidHealer);
        }

        public int tickInterval = 1000;

        public int radius = 1;

        public float healAmount = 0.1f;
    }
}
