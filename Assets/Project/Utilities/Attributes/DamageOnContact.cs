using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    private CreatureAttribute _creatureAttribute;
    //private Collider _collider;
    public int damage;
    public List<string> tags;

    // Start is called before the first frame update
    void Start()
    {
        //_creatureAttribute = GetComponent<CreatureAttribute>();
        //_collider = GetComponent<Collider>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Hit");
    //    if (tags.Contains(other.gameObject.tag))
    //    {
    //        _creatureAttribute = other.gameObject.GetComponent<CreatureAttribute>();
    //        _creatureAttribute.TakeNormalDamage(damage);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        if (tags.Contains(collision.gameObject.tag))
        {
            _creatureAttribute = collision.gameObject.GetComponent<CreatureAttribute>();
            if (_creatureAttribute==null)
            {
                _creatureAttribute = collision.gameObject.GetComponentInParent<CreatureAttribute>();
            }
            _creatureAttribute.TakeNormalDamage(damage);
        }
    }
}
