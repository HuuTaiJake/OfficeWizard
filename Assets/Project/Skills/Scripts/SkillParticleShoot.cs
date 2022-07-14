using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillParticleShoot", menuName = "Skills/SkillParticleShoot")]
public class SkillParticleShoot : SkillBehavior
{
    private GameObject player;
    public List<GameObject> particles;
    public List<Vector3> shootPoints;

    public override void Initialize(GameObject obj)
    {
        player = obj;
        return;
        //throw new System.NotImplementedException();
    }

    public override void TriggerSkill(JoystickManager joystickManager)
    {
        foreach (GameObject particle in particles)
        {
            Debug.Log("Skill Triggered!!!");
            GameObject _shotParticle = Instantiate(particle, joystickManager.GetSkillPosition(), Quaternion.identity) as GameObject;
        }

        //shootPoints[0]=joystickManager.GetSkillPosition();
        //foreach (Vector3 shootPoint in shootPoints)
        //{
        //    foreach (GameObject particle in particles)
        //    {
        //        Debug.Log("Skill Triggered!!!");
        //        GameObject _shotParticle = Instantiate(particle, shootPoint, Quaternion.identity) as GameObject;
        //        //_shotParticle.transform.rotation = Quaternion.LookRotation(joystickManager.GetSkillDirection());
        //    }
        //}
    }
}
