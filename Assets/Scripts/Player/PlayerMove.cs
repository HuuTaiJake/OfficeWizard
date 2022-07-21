using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Mirror;
using UnityEngine;

public class PlayerMove : MonoSingleton<PlayerMove>//NetworkBehaviour
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


    // Update is called once per frame
    private void Update()
    {
        _inputVertical = InputManager.Instance.GetVerticalAxis();
        _inputHorizontal = InputManager.Instance.GetHorizontalAxis();
    }

    private void FixedUpdate()
    {
        //if (this.isLocalPlayer)
        //{
            if (InputManager.Instance.GetGamemode() == Gamemode.Topdown)
            {
                transform.Translate(_inputHorizontal * _moveSpeed * Time.deltaTime, _inputVertical * _moveSpeed * Time.deltaTime, 0);
            }
            else
            {
                transform.Translate(_inputHorizontal * _moveSpeed * Time.deltaTime, 0, _inputVertical * _moveSpeed * Time.deltaTime);
            }
        //}
    }

    public void SwitchControl(PlayerMove from, PlayerMove to, float duration)
    {
        to.enabled = true;
        StartCoroutine(SwitchControlCoroutine(from, to, duration));
        from.enabled = false;
    }

    IEnumerator SwitchControlCoroutine(PlayerMove from, PlayerMove to, float duration)
    {
        if (duration <=0)
        {
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(duration);
        }
        from.enabled = true;
        to.enabled = false;
    }

}
