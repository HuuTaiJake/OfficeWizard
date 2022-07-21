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
    public GameObject _player;
    private GameObject _playerTransform;
    private bool _isReturn;
    private bool _isPulling;
    private bool _isGrabbing;

    private GameObject _grabbedObject;

    public RopeSkill.RopeType ropeType;


    // Start is called before the first frame update
    void Start()
    {
        _ropeSprites = new List<GameObject>();
        _isReturn = false;
        StartCoroutine(ShootDurationCounter());
        _startPosition = transform.position;


        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = _player.transform.Find("Skill Indicators/Shoot Point").gameObject;
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
        else if (_isPulling && ropeType == RopeSkill.RopeType.Pull)
        {
            PullRope();
        }
        else if (_isGrabbing && ropeType == RopeSkill.RopeType.Grab)
        {
            GrabRope();
        }
        else
        {
            CreateRope();
        }

        if (_ropeSprites.Count - 1 <= -1)
        {
            DestroyGameObject();
        }
    }

    private void PullRope()
    {
        _distance = Mathf.Abs(Vector3.Distance(_oldPosition, _playerTransform.transform.position));
        if (_distance >= 1)
        {
            _oldPosition += _unitDirection;
            if (_ropeSprites.Count > 0)
            {
                GameObject g = _ropeSprites[0];
                _ropeSprites.Remove(g);
                Destroy(g);
            }
        }
    }

    private void GrabRope()
    {

        _distance = Mathf.Abs(Vector3.Distance(_oldPosition, _grabbedObject.transform.position));
        if (_distance >= 1)
        {
            _oldPosition -= _unitDirection;
            if (_ropeSprites.Count > 0) {
                GameObject g = _ropeSprites[_ropeSprites.Count - 1];
                _ropeSprites.Remove(g);
                Destroy(g);
            }

            //_distance = Mathf.Abs(Vector3.Distance(_oldPosition, _grabbedObject.transform.position));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall") && ropeType == RopeSkill.RopeType.Pull)
        {
            _isPulling = true;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            _oldPosition = _player.transform.position - _unitDirection;
            _player.transform.DOMove(transform.position, 0.2f).OnComplete(DestroyGameObject);
        }
        else if (other.gameObject.tag == "Enemy" && ropeType == RopeSkill.RopeType.Grab)
        {
            _isGrabbing = true;
            _rigidbody.velocity = -_rigidbody.velocity;
            //_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            _grabbedObject = other.gameObject;
            _oldPosition = _grabbedObject.transform.position;
            if (_grabbedObject.transform.position != _player.transform.position)
            {
                transform.transform.DOMove(_playerTransform.transform.position, 0.2f).OnComplete(DestroyGameObject);
                _grabbedObject.transform.DOMove(_player.transform.position, 0.2f).OnComplete(DestroyGameObject);
            }
            else
            {
                DestroyGameObject();
            }
        }
        else if (other.gameObject.tag != "Player" && other.gameObject.layer != LayerMask.NameToLayer("Default") && !_isReturn)
        {
            _isReturn = true;
            _endPosition = _startPosition + _unitDirection;
            _rigidbody.velocity = -_rigidbody.velocity;
        }
    }

    private void LetPlayerMove()
    {
        _player.GetComponent<PlayerMove>().enabled = true;
        //_grabbedObject = null;
        //Destroy(gameObject.transform.parent.gameObject);
    }

    private void HideGameObject()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void DestroyGameObject()
    {
        LetPlayerMove();
        //DOTween.KillAll();
        Destroy(gameObject.transform.parent.gameObject);
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
        if (_isPulling || _isGrabbing)
        {
            _isReturn = false;
            yield break;
        }
        else if (_isReturn)
        {
            yield break;
        }
        else
        {
            _isReturn = true;
            _rigidbody.velocity = -_rigidbody.velocity;
        }
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
            if (_ropeSprites.Count > 0)
            {
                GameObject g = _ropeSprites[_ropeSprites.Count - 1];
                _ropeSprites.Remove(g);
                Destroy(g);
            }
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
