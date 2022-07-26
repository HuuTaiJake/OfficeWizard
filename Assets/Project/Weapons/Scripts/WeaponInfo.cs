using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEditor;



public class WeaponInfo : MonoBehaviour
{
    public WeaponConfig weaponConfig;
    private SpriteRenderer _weaponSprite;
    [SerializeField] private List<GameObject> _joysticks;
    // Start is called before the first frame update
    void Start()
    {
        _weaponSprite = transform.Find("Frame/Sprite").GetComponent<SpriteRenderer>();
        _weaponSprite.sprite = weaponConfig.weaponSprite;
        SetupJoystick();
    }
    
    
    private void SetupJoystick()
    {
        var _joystickAsset = Resources.Load("Joysticks/Skill Joystick") as GameObject;
        GameObject skillJoystick = GameObject.Find("Skill Joystick");
        foreach (SkillBehavior skill in weaponConfig.skills)
        {
            GameObject currentJoystick = Instantiate(_joystickAsset, Vector3.zero, Quaternion.identity);
            _joysticks.Add(currentJoystick);
            currentJoystick.transform.SetParent(skillJoystick.transform);

            JoystickManager joystickManager = currentJoystick.GetComponent<JoystickManager>();
            joystickManager.SetSkill(skill);
            currentJoystick.AddComponent<SkillCooldown>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
