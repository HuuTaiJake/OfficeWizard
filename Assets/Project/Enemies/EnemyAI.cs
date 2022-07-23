using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //[HideInInspector]public float speed;
    [HideInInspector]public GameObject target;
    public float speed;
    private UnityEngine.AI.NavMeshAgent _navMeshAgent;
    [SerializeField]private float _mininumDistance;
    [SerializeField]private float _maximumDistance;
    private CreatureAttribute _creatureAttri;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;


    private void Start()
    {
        _creatureAttri = GetComponent<CreatureAttribute>();
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _rigidbody2D = GetComponent<Rigidbody2D>();


        _spriteRenderer = GetComponent<SpriteRenderer>();


        _navMeshAgent.speed = speed;
        //speed = _creatureAttri.speed;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        speed = _creatureAttri.speed;
        _navMeshAgent.speed = speed;

        /*
        //move to player
        if (Vector2.Distance(transform.position, target.transform.position) > AI.GetMinDistance() ||
            Vector2.Distance(transform.position, target.transform.position) < AI.GetMaxDistance())
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
        {
            //Attack();
        }*/

        _spriteRenderer.flipX = target.transform.position.x > this.transform.position.x;
    }


    public float GetMaxDistance()
    {
        return _maximumDistance;
    }

    public float GetMinDistance()
    {
        return _mininumDistance;
    }
}
