using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillConfig", menuName = "SkillConfig")]
public class SkillConfig : ScriptableObject {
    public SkillType skillType;
    public float skillCooldown;
    public float skillRadius;
    public float skillRange;
    public JoystickType joystickType;

    public List<GameObject> bulletPrefabs;
    public Sprite icon;
}