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
    public class MentalState_LooseScrews : MentalState
    {
        public override void PostStart(string reason)
        {
            base.PostStart(reason);

            RattleScrew();
        }

        public void RattleScrew()
        {
            SkillRecord skill = pawn.skills.skills.Where(sr => !pawn.WorkTagIsDisabled(sr.def.disablingWorkTags)).RandomElement();

            if (skill.XpProgressPercent > 0.75f)
            {
                skill.xpSinceLastLevel = 0f;
            }
            else
            {
                skill.Level--;
            }
        }
    }
}
