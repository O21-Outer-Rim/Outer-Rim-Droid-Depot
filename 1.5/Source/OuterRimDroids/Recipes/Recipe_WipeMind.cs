using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using TabulaRasa;
using Asimov;

namespace OuterRimDroids
{
    public class Recipe_WipeDroid : Recipe_Surgery
    {
        public override bool AvailableOnNow(Thing thing, BodyPartRecord bodyPartRecord = null)
        {
            Pawn pawn;
            if ((pawn = (thing as Pawn)) == null)
            {
                return false;
            }
            if (pawn.def.HasModExtension<DefModExt_Droid>())
            {
                return true;
            }
            return false;
        }

        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            if (billDoer != null)
            {
                if (base.CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
                {
                    return;
                }
                TaleRecorder.RecordTale(TaleDefOf.DidSurgery, new object[] { billDoer, pawn });
                if (PawnUtility.ShouldSendNotificationAbout(pawn) || PawnUtility.ShouldSendNotificationAbout(billDoer))
                {
                    string text = this.recipe.successfullyRemovedHediffMessage.Formatted(billDoer.LabelShort, pawn.LabelShort);
                    Messages.Message(text, pawn, MessageTypeDefOf.PositiveEvent, true);
                }
            }
            CleanSlate(pawn, billDoer);
            Comp_TraitsOverTime traitComp = pawn.TryGetComp<Comp_TraitsOverTime>();
            if (traitComp != null)
            {
                traitComp.ResetTimer();
            }
        }

        public void CleanSlate(Pawn pawn, Pawn billDoer)
        {
            DefModExt_Droid modExt = pawn.def.GetModExtension<DefModExt_Droid>();

            ClearSkills(pawn, modExt);
            ClearTraits(pawn, modExt);

            // Remove all relations from the pawn.
            if (pawn.relations != null)
            {
                pawn.relations.ClearAllRelations();
            }

            pawn.SetFaction(Faction.OfPlayer, billDoer);
        }

        public void ClearSkills(Pawn pawn, DefModExt_Droid modExt)
        {
            // Restore skills to factory settings if they have any.
            // >Droid
            if (modExt != null)
            {
                if (modExt.useBackstoryForSkills)
                {
                    // Zero out skills.
                    for (int i = 0; i < pawn.skills.skills.Count(); i++)
                    {
                        pawn.skills.skills[i].Level = 0;
                    }
                    // Give Backstory Skills from Childhood.
                    foreach (SkillDef skill in pawn.story.GetBackstory(BackstorySlot.Childhood).skillGains.Keys)
                    {
                        pawn.skills.skills.Find(ss => ss.def == skill).Level += pawn.story.GetBackstory(BackstorySlot.Childhood).skillGains.TryGetValue(skill);
                    }
                    // Give Backstory Skills from Adulthood.
                    if (pawn.story.GetBackstory(BackstorySlot.Adulthood) != null)
                    {
                        foreach (SkillDef skill in pawn.story.GetBackstory(BackstorySlot.Adulthood).skillGains.Keys)
                        {
                            pawn.skills.skills.Find(ss => ss.def == skill).Level += pawn.story.GetBackstory(BackstorySlot.Adulthood).skillGains.TryGetValue(skill);
                        }
                    }
                }
                else
                {
                    // Zero out skills.
                    for (int i = 0; i < pawn.skills.skills.Count(); i++)
                    {
                        pawn.skills.skills[i].Level = 0;
                    }
                    // Give the factory skills.
                    Comp_Automaton comp = pawn.TryGetComp<Comp_Automaton>();
                    if(comp != null)
                    {
                        for (int i = 0; i < pawn.skills.skills.Count; i++)
                        {
                            SkillRecord sr = pawn.skills.skills[i];
                            if(comp.Props.skillSettings.Any(skr => skr.skill == sr.def))
                            {
                                sr.Level = comp.Props.skillSettings.Find(skr => skr.skill == sr.def).level;
                            }
                        }
                    }
                }
            }
        }

        public void ClearTraits(Pawn pawn, DefModExt_Droid modExt)
        {
            // Remove all traits from the pawn.
            pawn.story.traits.allTraits.RemoveAll(t => t is Trait);

            // Add factory fresh traits if they have any.
            // >Droid
            if (modExt != null)
            {
                List<TraitEntryAdvanced> traits = modExt.factorySettings?.traits;
                if (traits != null)
                {
                    for (int i = 0; i < traits.Count(); i++)
                    {
                        pawn.story.traits.allTraits.Add(new Trait(traits[i].def, traits[i].degree, true));
                    }
                }
            }
        }
    }
}
