using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    public WeaponID weaponID;
    public Sprite weaponSprite;
    public List<SkillBehavior> skills;
}
