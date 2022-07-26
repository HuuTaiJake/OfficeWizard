using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] Transform target;
    private EnemyAI AI;

    public GameObject projectile;
    public float timeBetweenShots;
    public float nextShotTime;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        AI = GetComponent<EnemyAI>();
    }

    private void Update()
    {

        if(Time.time > nextShotTime && Vector2.Distance(transform.position, target.transform.position) < AI.GetMaxDistance())
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextShotTime = Time.time + timeBetweenShots;
        }

    }
}
