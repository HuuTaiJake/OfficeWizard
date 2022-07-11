using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] public bool _isMobile = false;
    [SerializeField] public bool _isTopdown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance._isTopdown != _isTopdown)
        {
            _isTopdown = InputManager.Instance._isTopdown;
        }

        if (InputManager.Instance._isMobile != _isMobile)
        {
            _isMobile = InputManager.Instance._isMobile;
        }
    }
}
