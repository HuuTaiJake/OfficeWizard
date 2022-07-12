using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JoystickDefine
{
    Movement,
    Horizontal,
    Vertical,
    DirectionalSkill,
    DragAndDropSkill,
    TapSkill
}

public class JoystickManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private JoystickDefine _joystickDefine;
    [SerializeField] private float _joystickDragRange;

    [SerializeField] private Transform _skillIndicator;
    [SerializeField] private Transform _skillIndicatorRange;
    private Vector3 skillPosition;
    private Vector3 _lastSkillPosition;

    [SerializeField] private bool _isShowIndicator = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;//transform.root.gameObject.transform;
        skillPosition = _playerTransform.position;
    }

    void OnEnable()
    {
        _joystick.OnJoystickPointerUp += JoystickTrigger;
        _joystick.OnJoystickPointerUp += JoystickHideIndicator;
        _joystick.OnJoystickPointerDown += JoystickShowIndicator;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isShowIndicator)
        {
            // Calculate directional vector
            CalculateVector();

            // Directional skill
            if (_joystickDefine == JoystickDefine.DirectionalSkill)
            {
                DirectionalSkill();
            }

            // Drag and drop skill
            else if (_joystickDefine == JoystickDefine.DragAndDropSkill)
            {
                DragAndDropSkill();
            }
        }

        //Debug.DrawLine(_playerTransform.position, skillPosition, Color.red);
        //Debug.DrawLine(skillPosition + Vector3.right, skillPosition - Vector3.right, Color.blue);
    }

    private void JoystickTrigger()
    {
        Debug.Log("Shoot at:" + _lastSkillPosition);
    }

    private void JoystickShowIndicator()
    {
        _isShowIndicator = true;
        _skillIndicator.gameObject.SetActive(true);
        _skillIndicatorRange.gameObject.SetActive(true);
    }

    private void JoystickHideIndicator()
    {
        _isShowIndicator = false;
        _skillIndicator.gameObject.SetActive(false);
        _skillIndicatorRange.gameObject.SetActive(false);
    }

    private void CalculateVector()
    {
        if (InputManager.Instance.GetGamemode() == Gamemode.Topdown)
        {
            skillPosition.y = _playerTransform.position.y + _joystick.Vertical * _joystickDragRange;
        }
        else
        {
            skillPosition.z = _playerTransform.position.z + _joystick.Vertical * _joystickDragRange;
        }
        skillPosition.x = _playerTransform.position.x + _joystick.Horizontal * _joystickDragRange;
    }

    private void DirectionalSkill()
    {
        Vector3 skillDirection = skillPosition - _playerTransform.position;
        _skillIndicator.rotation = Quaternion.LookRotation(skillDirection);
        if (InputManager.Instance.GetGamemode() == Gamemode.Topdown)
        {
            _skillIndicator.rotation = _skillIndicator.rotation * Quaternion.AngleAxis(90, Vector3.forward);
        }
    }

    private void DragAndDropSkill()
    {
        _skillIndicatorRange.transform.localScale = new Vector3(_joystickDragRange, _joystickDragRange, 0);
        _skillIndicator.rotation = Quaternion.Euler(-90f, 0f, 0f);
        _skillIndicatorRange.rotation = Quaternion.Euler(-90f, 0f, 0f);
        _skillIndicator.transform.position = Vector3.MoveTowards(_skillIndicator.transform.position, skillPosition, 1f);
        _lastSkillPosition = skillPosition;
        if (InputManager.Instance.GetGamemode() == Gamemode.Topdown)
        {
            _skillIndicator.rotation = Quaternion.Euler(0f, 0f, 0f);
            _skillIndicatorRange.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
