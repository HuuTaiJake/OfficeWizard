using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float _inputVertical;
    private float _inputHorizontal;

    [SerializeField] private float _moveSpeed;
    private float _moveSpeedMax;

    [SerializeField] private bool _isTopdown;

    // Start is called before the first frame update
    private void Start()
    {
        _moveSpeedMax = _moveSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        _inputVertical = Input.GetAxis("Vertical");
        _inputHorizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space)) _isTopdown = !_isTopdown;
    }

    private void FixedUpdate()
    {
        if (_isTopdown)
        {
            transform.Translate(_inputHorizontal * _moveSpeed, _inputVertical * _moveSpeed, 0);
        }
        else
        {
            transform.Translate(_inputHorizontal * _moveSpeed, 0, _inputVertical * _moveSpeed);
        }
    }
}
