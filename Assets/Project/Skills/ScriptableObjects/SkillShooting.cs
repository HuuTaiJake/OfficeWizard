using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillShooting", menuName = "Skills/SkillShooting")]
public class SkillShooting : SkillBehavior
{
    public int skillDamage = 1;
    public GameObject player;
    public List<GameObject> projectiles;
    public List<GameObject> shootPoints;
    public override void Initialize(GameObject obj)
    {
        player = obj;
        return;
        //throw new System.NotImplementedException();
    }
    public override void TriggerSkill(JoystickManager joystickManager)
    {
        foreach (GameObject shootPoint in shootPoints)
        {
            foreach (GameObject projectile in projectiles)
            {
                GameObject _shotProjectile = Instantiate(projectile, player.transform.position, Quaternion.identity);
                Rigidbody _rigidbody;
                if (_shotProjectile.TryGetComponent(out Rigidbody _rigidbodyComponent))
                {
                    _rigidbody = _rigidbodyComponent;
                }
                else
                {
                    _rigidbody = _shotProjectile.AddComponent<Rigidbody>();
                    _rigidbody.useGravity = false;
                }
                
                //_shotProjectile.transform.rotation = Quaternion.LookRotation(joystickManager.GetSkillDirection());
                _rigidbody.velocity = joystickManager.GetSkillDirection() * skillForce;
            }
        }
    }
}