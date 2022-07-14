using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillParticleShootRope", menuName = "Skills/SkillParticleShootRope")]
public class SkillShootRope : SkillBehavior
{
    public GameObject rope;
    private GameObject player;

    public override void Initialize(GameObject obj)
    {
        player = obj;
        return;
        //throw new System.NotImplementedException();
    }

    public override void TriggerSkill(JoystickManager joystickManager)
    {
        GameObject G = Instantiate(rope, joystickManager.GetSkillPosition(), Quaternion.identity);
    }
}
