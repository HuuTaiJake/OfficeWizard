using System.Collections;
using System.Collections.Generic;
//using Unity.Netcode;
using UnityEngine;

public enum MovingDirection : int
{
    None,
    Down,
    Up,
    Left,
    Right
}

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private float _inputVertical;
    private float _inputHorizontal;
    private float _inputVerticalRaw;
    private float _inputHorizontalRaw;

    private Vector3 _lastPosition;
    private Vector3 _currentPosition;
    private Transform _transform;
    private bool _isMoving;
    private Coroutine _movingCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        _lastPosition = GetComponent<Transform>().position;
        _currentPosition = GetComponent<Transform>().position;
        _transform = GetComponent<Transform>();
        _isMoving = false;
        try
        {
            _animator = GetComponentInChildren<Animator>();
        }
        catch
        {

        }
    }

    private float GetHorizontalAxisRaw()
    {
        float lastPositionY = Mathf.Abs(_lastPosition.y);
        float lastPositionZ = Mathf.Abs(_lastPosition.z);
        Vector3 _lastDirection = new Vector3(0, 0, lastPositionZ);
        Vector3 _currentDirection = _transform.position - _lastPosition;

        if (InputManager.Instance.isTopdown == true)
        {
            _lastDirection = new Vector3(0, lastPositionY, 0);
        }

        if (Vector3.Dot(_lastDirection, _currentDirection) < 0)
            return -1;
        else if (Vector3.Dot(_lastDirection, _currentDirection) > 0)
            return 1;
        else
            return 0;
    }

    private float GetVerticalAxisRaw()
    {
        float lastPositionX = Mathf.Abs(_lastPosition.x);
        Vector3 _lastDirection = new Vector3(lastPositionX, 0, 0); ;
        Vector3 _currentDirection = _transform.position - _lastPosition;
        if (Vector3.Dot(_lastDirection, _currentDirection) < 0)
            return -1;
        else if (Vector3.Dot(_lastDirection, _currentDirection) > 0)
            return 1;
        else
            return 0;

    }

    // Update is called once per frame
    void Update()
    {
        _currentPosition = transform.position;

        if (transform.hasChanged)
        {
            if (_movingCoroutine != null)
            {
                StopCoroutine(_movingCoroutine);
                _movingCoroutine = null;
            }

            _isMoving = true;
            if (_animator.GetBool("Moving") != _isMoving)
            {
                _animator.SetBool("Moving", _isMoving);
            }

            _inputHorizontal = GetVerticalAxisRaw();
            _inputVertical = GetHorizontalAxisRaw();
            transform.hasChanged = false;
        }
        else if (_isMoving == true)
        {
            if (_movingCoroutine == null)
            {
                _movingCoroutine = StartCoroutine(StopMoving());
            }  
        }


        _animator.SetFloat("Vertical", _inputVertical);
        _animator.SetFloat("Horizontal", _inputHorizontal);

        if (_inputHorizontal != 0)
        {
            _inputHorizontalRaw = _inputHorizontal;
        }
        if (_inputVertical != 0)
        {
            _inputVerticalRaw = _inputVertical;
        }

        if (_inputVerticalRaw == -1 || _inputVerticalRaw == 1 || _inputHorizontalRaw == -1 || _inputHorizontalRaw == 1)
        {
            _animator.SetFloat("Last Vertical", _inputVertical);
            _animator.SetFloat("Last Horizontal", _inputHorizontal);
        }
        _lastPosition = transform.position;
    }

    IEnumerator StopMoving()
    {
        yield return new WaitForSeconds(0.02f);
        _isMoving = false;
        if (_animator.GetBool("Moving") != _isMoving)
        {
            _animator.SetBool("Moving", _isMoving);
        }
    }
    private void FixedUpdate()
    {

    }
}
