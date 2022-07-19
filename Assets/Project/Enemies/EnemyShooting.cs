using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    public GameObject projectile;
    public float timeBetweenShots;
    public float nextShotTime;

    private void Start()
    {
    }

    private void Update()
    {

        if(Time.time > nextShotTime)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextShotTime = Time.time + timeBetweenShots;
        }

    }
}
