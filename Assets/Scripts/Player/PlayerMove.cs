using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{
    private float _inputVertical;
    private float _inputHorizontal;

    [SerializeField] private float _moveSpeed;
    private float _moveSpeedMax;

    // Start is called before the first frame update
    private void Start()
    {
        _moveSpeedMax = _moveSpeed;
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) Destroy(this);
    }

    // Update is called once per frame
    private void Update()
    {
        _inputVertical = InputManager.Instance.GetVerticalAxis();
        _inputHorizontal = InputManager.Instance.GetHorizontalAxis();
    }

    private void FixedUpdate()
    {
        if (InputManager.Instance.GetGamemode()==Gamemode.Topdown)
        {
            transform.Translate(_inputHorizontal * _moveSpeed * Time.deltaTime, _inputVertical * _moveSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.Translate(_inputHorizontal * _moveSpeed * Time.deltaTime, 0, _inputVertical * _moveSpeed * Time.deltaTime);
        }
    }
}
