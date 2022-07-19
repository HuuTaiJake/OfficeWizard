using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    private Vector3 targetPosition;
    public float speed;

    private void Start()
    {
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        StartCoroutine(DestroyAfterDuration(3f));
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if(transform.position == targetPosition)
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
