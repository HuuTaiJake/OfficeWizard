using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    private GameObject _player;
    public float timeToDisappear;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        
        StartCoroutine(DestroyAfterDuration(0.2f));
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.FromToRotation(-Vector3.up, _player.transform.position - transform.position);
    }

    private void OnBecameVisible()
    {
        //transform.rotation = Quaternion.FromToRotation(-Vector3.up, _player.transform.position - transform.position);
    }
    IEnumerator DestroyAfterDuration(float duration)
    {
        transform.rotation = Quaternion.FromToRotation(-Vector3.up, _player.transform.position - transform.position);
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
