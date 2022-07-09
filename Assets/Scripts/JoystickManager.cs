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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (_joystickDefine)
        {
            case JoystickDefine.DirectionalSkill:
                {
                    //Vector3 verticalVector = Vector3.zero;
                    //Vector3 horizontalVector = Vector3.zero;
                    Vector3 skillDirection = playerTransform.position;
                    if (InputManager.Instance.GetGamemode() == Gamemode.Topdown)
                    {
                        //verticalVector = new Vector3(0, _joystick.Vertical, 0);
                        skillDirection.y = playerTransform.position.y + _joystick.Vertical;
                    }
                    else
                    {
                        //verticalVector = new Vector3(0, 0, _joystick.Vertical);
                        skillDirection.z = playerTransform.position.z + _joystick.Vertical;
                    }
                    skillDirection.x = playerTransform.position.x + _joystick.Horizontal;

                    Debug.DrawLine(playerTransform.position, skillDirection, Color.red);
                    break;
                }
            case JoystickDefine.DragAndDropSkill:
                {
                    Vector3 skillPosition = playerTransform.position;
                    if (InputManager.Instance.GetGamemode() == Gamemode.Topdown)
                    {
                        //verticalVector = new Vector3(0, _joystick.Vertical, 0);
                        skillPosition.y = playerTransform.position.y + _joystick.Vertical * _joystickDragRange;
                    }
                    else
                    {
                        //verticalVector = new Vector3(0, 0, _joystick.Vertical);
                        skillPosition.z = playerTransform.position.z + _joystick.Vertical * _joystickDragRange;
                    }
                    skillPosition.x = playerTransform.position.x + _joystick.Horizontal * _joystickDragRange;

                    Debug.DrawLine(playerTransform.position, skillPosition, Color.red);
                    Debug.DrawLine(skillPosition + Vector3.right, skillPosition - Vector3.right, Color.blue);
                    break;
                }

                
        }
        
    }
}
