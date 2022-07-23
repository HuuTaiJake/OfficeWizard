using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerisBoss : MonoBehaviour
{
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(_player.transform.position.x, transform.position.y, transform.position.z);
        StartCoroutine(Movement(0.5f));
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement(0.5f);
    }

    private IEnumerator Movement(float duration)
    {
        //transform.position = Vector3.Lerp(transform.position,new Vector3( _player.transform.position.x, transform.position.y, transform.position.z),2f);
        while (true)
        {
            yield return new WaitForSeconds(duration);
            if (transform.position.x < _player.transform.position.x)
            {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > _player.transform.position.x)
            {
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            }
        }
    }
}
