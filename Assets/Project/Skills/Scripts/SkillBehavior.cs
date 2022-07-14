using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBehavior : ScriptableObject
{
    public SkillID skillID;
    public JoystickDefine joystickDefine;
    public Sprite skillSprite;
    public AudioClip skillSound;
    public float skillBaseCooldown = 1f;

    public float skillSize;
    public float skillRange;
    public float skillForce;
    public float skillPower;

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerSkill(JoystickManager joystickManager);

}
