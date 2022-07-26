using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyShootPlayerInRange : MonoBehaviour
{

    public GameObject projectile;
    public float timeBetweenShots;
    public float nextShotTime;
    public float _range = 5f;

    public float _speed = 5f;
    GameObject _player;
 

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if (Time.time > nextShotTime && distance < _range)
        {
            Vector3 positon = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
            GameObject g = Instantiate(projectile, positon, Quaternion.identity);
            Sequence myTween = DOTween.Sequence();
            myTween.Append(g.transform.DOMove(_player.transform.position, _speed));
            myTween.Append(g.transform.DOScale(Vector3.zero, 0.5f));
            nextShotTime = Time.time + timeBetweenShots;
        }

    }

}
