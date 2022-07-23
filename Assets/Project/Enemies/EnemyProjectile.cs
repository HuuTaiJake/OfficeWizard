using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    private Vector3 _targetPosition;
    private Vector3 _direction;
    private Rigidbody2D _rigidBody;
    public float speed;

    private void Start()
    {
        _targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        _rigidBody = GetComponent<Rigidbody2D>();
        _direction = _targetPosition - transform.position;
        StartCoroutine(DestroyAfterDuration(3f));
    }

    private void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
        _rigidBody.velocity = _direction.normalized * speed;
        if(transform.position == _targetPosition)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }
}
