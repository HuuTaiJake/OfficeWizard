using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
        GameObject _joystickAsset = AssetDatabase.LoadAssetAtPath("Assets/Project/Prefabs/Skill Joystick.prefab", typeof(Object)) as GameObject;
        GameObject skillJoystick = GameObject.Find("Skill Joystick");
        foreach (SkillBehavior skill in weaponConfig.skills)
        {
            GameObject currentJoystick = Instantiate(_joystickAsset, Vector3.zero, Quaternion.identity);
            _joysticks.Add(currentJoystick);
            currentJoystick.transform.parent = skillJoystick.transform;

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
