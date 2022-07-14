using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum JoystickDefine
{
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
    [SerializeField] private float _skillRange;
    [SerializeField] private float _skillSize;

    [SerializeField] private Transform _skillIndicatorPosition;
    [SerializeField] private Transform _skillIndicatorRange;

    public Vector3 skillPosition;
    public Vector3 skillDirection;
    private Vector3 _lastSkillPosition;
    [SerializeField] private SkillBehavior _skill;

    [SerializeField] private bool _isShowIndicator = false;

    public UnityAction OnJoystickTrigger;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_joystickDefine == JoystickDefine.DirectionalSkill)
        {
            _skillIndicatorRange = GameObject.FindGameObjectWithTag("Player").transform.Find("Skill Indicators/Directional/Range");
        }
        else if (_joystickDefine == JoystickDefine.DragAndDropSkill)
        {
            _skillIndicatorRange = GameObject.FindGameObjectWithTag("Player").transform.Find("Skill Indicators/Drag And Drop/Range");
            _skillIndicatorPosition = GameObject.FindGameObjectWithTag("Player").transform.Find("Skill Indicators/Drag And Drop/Position");

        }

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
        OnJoystickTrigger?.Invoke();
        _skill.Initialize(_player);
        //_skill.TriggerSkill(gameObject.GetComponent<JoystickManager>());
        Debug.Log("Shoot at:" + skillDirection);
    }

    private void JoystickShowIndicator()
    {
        //ResetSkillDirection();
        _isShowIndicator = true;
        if (_skillIndicatorPosition != null)
        {
            _skillIndicatorPosition.gameObject.SetActive(true);
        }
        if (_skillIndicatorRange != null)
        {
            _skillIndicatorRange.gameObject.SetActive(true);
        }
    }

    private void JoystickHideIndicator()
    {
        _isShowIndicator = false;
        if (_skillIndicatorPosition != null)
        {
            _skillIndicatorPosition.gameObject.SetActive(false);
        }
        if (_skillIndicatorRange != null)
        {
            _skillIndicatorRange.gameObject.SetActive(false);
        }
    }

    private void CalculateVector()
    {
        if (InputManager.Instance.GetGamemode() == Gamemode.Topdown)
        {
            skillPosition.y = _playerTransform.position.y + _joystick.Vertical * _skillRange;
            skillPosition.z = _playerTransform.position.z;
        }
        else
        {
            skillPosition.y = _playerTransform.position.y;
            skillPosition.z = _playerTransform.position.z + _joystick.Vertical * _skillRange;
        }
        skillPosition.x = _playerTransform.position.x + _joystick.Horizontal * _skillRange;
    }

    private void DirectionalSkill()
    {
        _skillIndicatorRange.transform.localScale = new Vector3(_skillRange, _skillRange, 0);

        //Set the direction, rotation
        skillDirection = skillPosition - _playerTransform.position;

        if (InputManager.Instance.GetGamemode() == Gamemode.Topdown)
        {
            _skillIndicatorRange.rotation = Quaternion.LookRotation(skillDirection);
            _skillIndicatorRange.rotation = _skillIndicatorRange.rotation * Quaternion.FromToRotation(Vector3.right, Vector3.forward);
        }
        else
        {
            _skillIndicatorRange.rotation = Quaternion.LookRotation(skillDirection);
            _skillIndicatorRange.rotation = _skillIndicatorRange.rotation * Quaternion.AngleAxis(90, Vector3.right) * Quaternion.AngleAxis(90, Vector3.forward);
        }
        //Debug.DrawLine(_playerTransform.position, _skillIndicatorRange.right);
        Debug.DrawLine(_skillIndicatorRange.position, _skillIndicatorRange.right);
    }

    private void DragAndDropSkill()
    {

        _skillIndicatorRange.transform.localScale = new Vector3(_skillRange, _skillRange, 0);
        _skillIndicatorPosition.transform.localScale = new Vector3(_skillSize, _skillSize, 0);

        //Set the direction, rotation
        _skillIndicatorPosition.rotation = Quaternion.Euler(-90f, 0f, 0f);
        _skillIndicatorRange.rotation = Quaternion.Euler(-90f, 0f, 0f);
        _skillIndicatorPosition.transform.position = Vector3.MoveTowards(_skillIndicatorPosition.transform.position, skillPosition, 1f);
        _lastSkillPosition = skillPosition;
        skillDirection = skillPosition - _playerTransform.position;
        if (InputManager.Instance.GetGamemode() == Gamemode.Topdown)
        {
            _skillIndicatorPosition.rotation = Quaternion.Euler(0f, 0f, 0f);
            _skillIndicatorRange.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        Debug.DrawLine(_playerTransform.position, _skillIndicatorPosition.right);
        Debug.DrawLine(_playerTransform.position, skillDirection);
    }
    public void SetSkill(SkillBehavior skill)
    {
        _skill = skill;
        _joystickDefine = skill.joystickDefine;
        _skillRange = skill.skillRange;
        _skillSize = skill.skillSize;
    }
    public SkillBehavior GetSkill()
    {
        return _skill;
    }
    public Vector3 GetSkillDirection()
    {
        return skillDirection;
    }

    private void ResetSkillDirection()
    {
        //skillDirection = Vector3.zero;
        if (_skillIndicatorPosition!=null)
        {
            _skillIndicatorPosition.rotation = Quaternion.Euler(0f, 0f, 0f);  
        }
        if (_skillIndicatorRange!=null)
        {
            _skillIndicatorRange.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
