using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    [HideInInspector]public GameObject target;
    public float mininumDistance;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //move to player
        if (Vector2.Distance(transform.position, target.transform.position) > mininumDistance) 
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
        {
            //Attack();
        }
    }
}
