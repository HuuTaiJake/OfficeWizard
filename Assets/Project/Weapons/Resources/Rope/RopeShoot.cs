using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class RopeShoot : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _ropeSprite;
    private List<GameObject> _ropeSprites;
    [SerializeField] private GameObject _ropesLayout;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _unitDirection;
    private Vector3 _oldPosition;

    private Rigidbody _rigidbody;
    private float _distance;
    private GameObject _player;
    private bool _isReturn;
    private bool _isPulling;

    // Start is called before the first frame update
    void  Start()
    {
        _ropeSprites = new List<GameObject>();
        _isReturn = false;
        StartCoroutine(ShootDurationCounter());
        _startPosition = transform.position;

        _player = GameObject.FindGameObjectWithTag("Player");
        StopPlayerMove();
        InstantiateRope();

        _rigidbody = GetComponent<Rigidbody>();
        _unitDirection = _rigidbody.velocity.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isReturn)
        {
            DeleteRope();
        }
        else if (_isPulling)
        {
            PullRope();
        }
        else 
        {
            CreateRope();
        }

        if (_ropeSprites.Count - 1 == -1)
        {
            LetPlayerMove();
            Destroy(gameObject);
        }
    }

    private void PullRope()
    {
        _distance = Mathf.Abs(Vector3.Distance(_oldPosition, _player.transform.position));
        if (_distance >= 1)
        {
            _oldPosition += _unitDirection;
            GameObject g = _ropeSprites[0];
            _ropeSprites.Remove(g);
            Destroy(g);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            _isPulling = true;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            _oldPosition = _player.transform.position - _unitDirection;
            _player.transform.DOMove(transform.position, 0.2f).OnComplete(LetPlayerMove);
            
        }
    }




    private void LetPlayerMove()
    {
        _player.GetComponent<PlayerMove>().enabled = true;
    }
    private void StopPlayerMove()
    {
        _player.GetComponent<PlayerMove>().enabled = false;
    }

    IEnumerator ShootDurationCounter()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponentInChildren<Collider>().enabled = false;
        _endPosition = _startPosition + _unitDirection;
        _isReturn = true;
        _rigidbody.velocity = -_rigidbody.velocity;
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, -transform.up);

    }

    private void CreateRope()
    {
        _distance = Mathf.Abs(Vector3.Distance(_startPosition, transform.position));
        if (_distance >= 1)
        {
            _startPosition += _unitDirection;
            InstantiateRope();
        }
    }

    private void DeleteRope()
    {
        _distance = Mathf.Abs(Vector3.Distance(_endPosition, transform.position));
        if (_distance >= 1)
        {
            _endPosition -= _unitDirection;
            GameObject g = _ropeSprites[_ropeSprites.Count-1];
            _ropeSprites.Remove(g);
            Destroy(g);
        }
    }

    private void InstantiateRope()
    {
        GameObject g = Instantiate(_ropeSprite, _startPosition, Quaternion.identity);
        g.transform.SetParent(_ropesLayout.transform);
        g.transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.up);
        _ropeSprites.Add(g);
    }

}
