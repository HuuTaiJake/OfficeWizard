using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillController", menuName = "Skills/SkillController")]
public class ControllerSkill : SkillBehavior
{
    private GameObject player;
    public float duration;
    GameObject nearestGameObject = null;
    float distance = 0;
    float newDistance = 0;
    public string tag = "";

    public override void Initialize(GameObject playerObject)
    {
        player = playerObject;
        return;
    }

    public override void TriggerSkill(JoystickManager joystickManager)
    {
        Collider[] inRangeColliders = Physics.OverlapSphere(player.transform.position, skillRange);
        nearestGameObject = null;

        foreach (var collider in inRangeColliders)
        {
            if (collider.gameObject.tag == tag)
            {
                newDistance = Mathf.Abs(Vector3.Distance(collider.transform.position,player.transform.position));
                if (nearestGameObject == null || newDistance < distance)
                {
                    nearestGameObject = collider.gameObject;
                    distance = newDistance;
                    Debug.Log(nearestGameObject);
                }

            }
        }

        if (nearestGameObject!=null)
        {
            PlayerMove from = player.GetComponent<PlayerMove>();
            PlayerMove to = nearestGameObject.GetComponent<PlayerMove>();
            to.SwitchControl(from,to,duration);
        }
    }
}
