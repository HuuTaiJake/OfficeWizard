using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RopeSkill", menuName = "Skills/SkillShootRope")]
public class RopeSkill : SkillBehavior
{
    public enum RopeType
    {
        Grab,
        Pull
    }
    [SerializeField] private RopeType _ropeType;
    private GameObject player;
    public GameObject rope;
    private GameObject shootpoint;
    public Sprite tipSprite;

    public override void Initialize(GameObject playerObject)
    {
        
        //player = playerObject;
        shootpoint = GameObject.FindGameObjectWithTag("Player").transform.Find("Skill Indicators/Shoot Point").gameObject;
        player = shootpoint;
        return;
        //throw new System.NotImplementedException();
    }
    public override void TriggerSkill(JoystickManager joystickManager)
    {
        Vector3 _direction = joystickManager.GetSkillDirection();
        GameObject _shotProjectile = Instantiate(rope, shootpoint.transform.position, Quaternion.identity);
        _shotProjectile.SetActive(false);
        _shotProjectile.transform.rotation = Quaternion.FromToRotation(Vector3.up, _direction);
        _shotProjectile.GetComponentInChildren<Rigidbody>().velocity = _direction.normalized * skillForce;
        _shotProjectile.GetComponentInChildren<RopeShoot>().ropeType = _ropeType;
        _shotProjectile.GetComponentInChildren<SpriteRenderer>().sprite = tipSprite;
        _shotProjectile.SetActive(true);
    }
}
