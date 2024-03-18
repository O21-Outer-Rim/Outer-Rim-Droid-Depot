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
    public class CompUseEffect_ChangeSkillLevel : CompUseEffect
	{
		private const float XPGainAmount = 50000f;

		private SkillDef Skill => parent.GetComp<Comp_SkillDisk>().skill;

		public override void DoEffect(Pawn user)
		{
			base.DoEffect(user);
			SkillDef skill = Skill;
			int level = user.skills.GetSkill(skill).GetLevel(includeAptitudes: false);
			user.skills.GetSkill(skill).Level += 1;
			int level2 = user.skills.GetSkill(skill).GetLevel(includeAptitudes: false);
			if (PawnUtility.ShouldSendNotificationAbout(user))
			{
				Messages.Message("OuterRimDroids.SkillDataDiskUsed".Translate(user.LabelShort, skill.LabelCap, level, level2, user.Named("USER")), user, MessageTypeDefOf.PositiveEvent);
			}
		}

		public override bool CanBeUsedBy(Pawn p, out string failReason)
		{
			if (p.skills == null)
			{
				failReason = null;
				return false;
			}
			if (p.skills.GetSkill(Skill).TotallyDisabled)
			{
				failReason = "SkillDisabled".Translate();
				return false;
			}
			return base.CanBeUsedBy(p, out failReason);
		}
	}
}
