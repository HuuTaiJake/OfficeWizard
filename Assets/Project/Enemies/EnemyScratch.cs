using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScratch : MonoBehaviour
{
    private GameObject player;
    public GameObject scratchSkill;
    public float timeBetweenHit;
    public float nextHitTime;
    private EnemyAI AI;

    // Start is called before the first frame update
    void Start()
    {
        AI = GetComponent<EnemyAI>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {


        if (Time.time > nextHitTime && (Vector2.Distance(transform.position, player.transform.position) <= AI.GetMinDistance()))
        {
            GameObject g = Instantiate(scratchSkill, player.transform.position , Quaternion.identity);

            nextHitTime = Time.time + timeBetweenHit;
        }

    }



}
