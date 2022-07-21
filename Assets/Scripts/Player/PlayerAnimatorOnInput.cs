using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorOnInput : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private float _inputVertical;
    private float _inputHorizontal;
    private float _inputVerticalRaw;
    private float _inputHorizontalRaw;
    private bool _isMoving;
    private bool _isHolding;
    private PlayerMove _playerMove;


    // Start is called before the first frame update
    void Start()
    {
        _playerMove = gameObject.GetComponent<PlayerMove>();
        _isMoving = false;
        try
        {
            _animator = transform.Find("Player Sprite").GetComponent<Animator>();
        }
        catch
        {

        }
    }
    private void OnEnable()
    {
        InputManager.Instance.OnSkillTrigger += SkillAnimation;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerMove.enabled == true)
        {
            if (_isHolding == true)
            {
                _isHolding = false;
                _animator.SetBool("Skill Hold", _isHolding);
            }

            _inputVertical = InputManager.Instance.GetVerticalAxis();
            _inputHorizontal = InputManager.Instance.GetHorizontalAxis();
            _inputVerticalRaw = InputManager.Instance.GetVerticalAxisRaw();
            _inputHorizontalRaw = InputManager.Instance.GetHorizontalAxisRaw();

            _animator.SetFloat("Vertical", _inputVertical);
            _animator.SetFloat("Horizontal", _inputHorizontal);

            if (_inputVertical != 0 || _inputHorizontal != 0)
            {
                _isMoving = true;
                _animator.SetBool("Moving", _isMoving);
            }
            else if (_isMoving == true)
            {
                _isMoving = false;
                _animator.SetBool("Moving", _isMoving);
            }

            if (_inputVerticalRaw == -1 || _inputVerticalRaw == 1 || _inputHorizontalRaw == -1 || _inputHorizontalRaw == 1)
            {
                _animator.SetFloat("Last Vertical", _inputVerticalRaw);
                _animator.SetFloat("Last Horizontal", _inputHorizontalRaw);
            }
        }
        else if (_playerMove.enabled == false && _isHolding == false)
        {
            _isHolding = true;
            _animator.SetBool("Skill Hold", _isHolding);
        }

    }

    void SkillAnimation()
    {
        _animator.SetTrigger("Skill");
    }

}
