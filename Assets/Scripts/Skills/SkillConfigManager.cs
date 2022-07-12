using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillConfigManager : MonoSingleton<SkillConfigManager>
{
	[SerializeField] private List<SkillConfig> skillConfigs;

	public SkillConfig GetSkillConfig(SkillType skillType)
	{
		return this.skillConfigs.First(r => r.skillType == skillType);
	}
}
