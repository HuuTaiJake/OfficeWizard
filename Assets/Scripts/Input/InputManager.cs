using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gamemode
{
    Normal,
    Topdown,
    TPS,
    FPS,
}
public class InputManager : MonoSingleton<InputManager>
{
    [SerializeField] private FixedJoystick _joystick;
    //[SerializeField] private JoystickDefine _joystickDefine;
    private Gamemode _gamemode;
    private float _inputVertical;
    private float _inputHorizontal;
    private float _inputVerticalRaw;
    private float _inputHorizontalRaw;
    [SerializeField] public bool _isTopdown;
    [SerializeField] public bool _isMobile;

    private void Start()
    {
        _gamemode = Gamemode.Normal;
        //_joystick = GetComponent<FixedJoystick>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) _isTopdown = !_isTopdown;
        if (Input.GetKeyDown(KeyCode.Return)) _isMobile = !_isMobile;

        if (_isMobile)
        {
            _inputVertical = _joystick.Vertical;
            _inputHorizontal = _joystick.Horizontal;
            _inputVerticalRaw = _joystick.Vertical > 0 ? 1 : _joystick.Vertical < 0 ? -1 : 0;
            _inputHorizontalRaw = _joystick.Horizontal > 0 ? 1 : _joystick.Horizontal < 0 ? -1 : 0;
        }
        else
        {
            _inputVertical = Input.GetAxis("Vertical");
            _inputHorizontal = Input.GetAxis("Horizontal");
            _inputVerticalRaw = Input.GetAxisRaw("Vertical");
            _inputHorizontalRaw = Input.GetAxisRaw("Horizontal");
        }


    }

    public float GetHorizontalAxis()
    {
        return _inputHorizontal;
    }

    public float GetVerticalAxis()
    {
        return _inputVertical;
    }

    public float GetHorizontalAxisRaw()
    {
        return _inputHorizontalRaw;
    }

    public float GetVerticalAxisRaw()
    {
        return _inputVerticalRaw;
    }

    public Gamemode GetGamemode()
    {
        if (_isTopdown) _gamemode = Gamemode.Topdown;
        else _gamemode = Gamemode.Normal;
        return _gamemode;
    }
}
