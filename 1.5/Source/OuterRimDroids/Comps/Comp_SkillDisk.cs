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
    public class Comp_SkillDisk : CompUsable
	{
		public SkillDef skill;

		public override void PostExposeData()
		{
			base.PostExposeData();
			Scribe_Defs.Look(ref skill, "skill");
		}

		public override void Initialize(CompProperties props)
		{
			base.Initialize(props);
			CompProperties_SkillDisk skillDiskComp = (CompProperties_SkillDisk)props;
			skill = skillDiskComp.skill;
		}

        public override bool CanBeUsedBy(Pawn p, out string failReason)
        {
            if (p.def.HasModExtension<DefModExt_DroidEquipment>())
			{
				return base.CanBeUsedBy(p, out failReason);
			}
			failReason = "OuterRimDroids.CantEquipReasonNotDroid".Translate();
			return false;
		}

        public override string FloatMenuOptionLabel(Pawn pawn)
		{
			return string.Format(base.Props.useLabel, skill.skillLabel);
		}

		public override bool AllowStackWith(Thing other)
		{
			if (!base.AllowStackWith(other))
			{
				return false;
			}
			Comp_SkillDisk compSkillDisk = other.TryGetComp<Comp_SkillDisk>();
			if (compSkillDisk == null || compSkillDisk.skill != skill)
			{
				return false;
			}
			return true;
		}

		public override void PostSplitOff(Thing piece)
		{
			base.PostSplitOff(piece);
			Comp_SkillDisk compSkillDisk = piece.TryGetComp<Comp_SkillDisk>();
			if (compSkillDisk != null)
			{
				compSkillDisk.skill = skill;
			}
		}
	}
}
