using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [HideInInspector]public float speed;
    [HideInInspector]public GameObject target;
    [SerializeField]private float _mininumDistance;
    [SerializeField]private float _maximumDistance;
    private CreatureAttribute _creatureAttri; 

    private void Start()
    {
        _creatureAttri = GetComponent<CreatureAttribute>();
        speed = _creatureAttri.speed;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //move to player
        if (Vector2.Distance(transform.position, target.transform.position) > _mininumDistance ||
            Vector2.Distance(transform.position, target.transform.position) < _maximumDistance) 
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
        {
            //Attack();
        }
    }
}
