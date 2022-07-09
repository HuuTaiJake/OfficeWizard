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

    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private JoystickDefine _joystickDefine;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float _joystickDragRange;
    [SerializeField] private Transform _skillIndicator;
    [SerializeField] private Transform _skillIndicatorRange;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Calculate directional vector
        Vector3 skillPosition = playerTransform.position;
        if (InputManager.Instance.GetGamemode() == Gamemode.Topdown)
        {
            skillPosition.y = playerTransform.position.y + _joystick.Vertical * _joystickDragRange;
        }
        else
        {
            skillPosition.z = playerTransform.position.z + _joystick.Vertical * _joystickDragRange;
        }
        skillPosition.x = playerTransform.position.x + _joystick.Horizontal * _joystickDragRange;

        // Directional skill
        if (_joystickDefine == JoystickDefine.DirectionalSkill)
        {
            Vector3 skillDirection = skillPosition - playerTransform.position;
            _skillIndicator.rotation = Quaternion.LookRotation(skillDirection);
            if (InputManager.Instance.GetGamemode() == Gamemode.Topdown)
            {
                _skillIndicator.rotation = _skillIndicator.rotation * Quaternion.AngleAxis(90, Vector3.forward);
            }
        }

        // Drag and drop skill
        if (_joystickDefine == JoystickDefine.DragAndDropSkill)
        {

            _skillIndicatorRange.transform.localScale = new Vector3(_joystickDragRange, _joystickDragRange, 0);
            _skillIndicator.rotation = Quaternion.Euler(-90f, 0f, 0f);
            _skillIndicatorRange.rotation = Quaternion.Euler(-90f, 0f, 0f);
            _skillIndicator.transform.position = Vector3.MoveTowards(_skillIndicator.transform.position, skillPosition, 1f);
            if (InputManager.Instance.GetGamemode() == Gamemode.Topdown)
            {
                _skillIndicator.rotation = Quaternion.Euler(0f, 0f, 0f) ;
                _skillIndicatorRange.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }


        //Debug.DrawLine(playerTransform.position, skillPosition, Color.red);
        //Debug.DrawLine(skillPosition + Vector3.right, skillPosition - Vector3.right, Color.blue);
    }
}
