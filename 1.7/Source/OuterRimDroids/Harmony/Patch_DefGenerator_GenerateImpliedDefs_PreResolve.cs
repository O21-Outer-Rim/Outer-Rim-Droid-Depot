using HarmonyLib;
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
    [HarmonyPatch(typeof(DefGenerator), "GenerateImpliedDefs_PreResolve")]
    public static class Patch_DefGenerator_GenerateImpliedDefs_PreResolve
    {
        [HarmonyPrefix]
        public static void Prefix()
        {
            foreach (ThingDef item in ImpliedThingDefs())
            {
                DefGenerator.AddImpliedDef(item);
            }
        }

        public static IEnumerable<ThingDef> ImpliedThingDefs()
        {
            foreach(SkillDef skill in DefDatabase<SkillDef>.AllDefs)
            {
                yield return GenerateSkillDatadisk(skill);
            }
            yield break;
        }

        public static ThingDef GenerateSkillDatadisk(SkillDef skill)
        {
            return new ThingDef()
            {
                defName = "OuterRim_SkillDatadisk_" + skill.defName,
                label = "OuterRimDroids.SkillDataDiskLabel".Translate(skill.label),
                description = "OuterRimDroids.SkillDataDiskDesc".Translate(),
                graphicData = new GraphicData
                {
                    texPath = "OuterRim/Items/DroidProgram",
                    graphicClass = typeof(Graphic_Random)
                },
                techLevel = TechLevel.Ultra,
                altitudeLayer = AltitudeLayer.Item,
                alwaysHaulable = true,
                rotatable = false,
                pathCost = DefGenerator.StandardItemPathCost,
                tradeTags = new List<string> { "ORDroidSkillDisks", "ExoticMisc" },
                stackLimit = 1,
                tradeNeverStack = true,
                forceDebugSpawnable = true,
                drawerType = DrawerType.MapMeshOnly,
                category = ThingCategory.Item,
                selectable = true,
                thingClass = typeof(ThingWithComps),
                statBases = new List<StatModifier>
                {
                    new StatModifier
                    {
                        stat = StatDefOf.MaxHitPoints,
                        value = 80f
                    },
                    new StatModifier
                    {
                        stat = StatDefOf.Mass,
                        value = 0.2f
                    },
                    new StatModifier
                    {
                        stat = StatDefOf.DeteriorationRate,
                        value = 2f
                    },
                    new StatModifier
                    {
                        stat = StatDefOf.Flammability,
                        value = 0.2f
                    },
                    new StatModifier
                    {
                        stat = StatDefOf.MarketValue,
                        value = 750f
                    }
                },
                comps = new List<CompProperties>
                {
                    new CompProperties_UseEffectPlaySound
                    {
                        // TODO replace with a more star warsy droid sound.
                        soundOnUsed = SoundDefOf.MechSerumUsed
                    },
                    new CompProperties_UseEffectDestroySelf
                    {
                        compClass = typeof(CompUseEffect_DestroySelf)
                    },
                    new CompProperties_Forbiddable(),
                    new CompProperties_SkillDisk
                    {
                        compClass = typeof(Comp_SkillDisk),
                        useJob = OuterRimDroidsDefOf.OuterRim_UseSkillDatadisk,
                        useLabel = "OuterRimDroids.SkillDataDiskUse".Translate(skill.label),
                        skill = skill
                    },
                    new CompProperties_UseEffect
                    {
                        compClass = typeof(CompUseEffect_ChangeSkillLevel)
                    },
                },
                thingCategories = new List<ThingCategoryDef> { OuterRimDroidsDefOf.OuterRim_SkillDatadisks },
                thingSetMakerTags = new List<string> { "RewardStandardMidFreq", "SkillNeurotrainer" },
                modContentPack = OuterRimDroidsMod.mod.Content
            };
        }
    }
}
