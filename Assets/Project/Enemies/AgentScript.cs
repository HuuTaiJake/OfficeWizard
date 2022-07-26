using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    [SerializeField] Transform target;

    private NavMeshAgent agent;

    private EnemyAI AI;

    public 

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        AI = GetComponent<EnemyAI>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {

        //move to player
        if (Vector2.Distance(transform.position, target.transform.position) > AI.GetMinDistance() &&
            Vector2.Distance(transform.position, target.transform.position) < AI.GetMaxDistance())
        {
            agent.SetDestination(target.position);
        }
        else
        {
            //Attack();
        }

    }
}
