using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillCooldown : MonoBehaviour
{
    public JoystickManager joystick;
    public Image _skillMask;
    public TextMeshProUGUI skillCoolDownText;

    private SkillBehavior _skill;
    public Image _skillSprite;
    private GameObject _skillHolder;
    private AudioSource _skillSound;
    private float _skillCooldownDuration;
    private float _skillCooldownReadyTime;
    private float _skillCooldownTimeLeft;

    private bool joystickTriggered;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        _skillHolder = transform.Find("Handle").gameObject;
        _skillSprite = _skillHolder.transform.Find("Sprite").GetComponent<Image>();
        _skillMask = _skillHolder.transform.Find("Mask").GetComponent<Image>();
        skillCoolDownText = GetComponentInChildren<TextMeshProUGUI>();
        joystick = GetComponent<JoystickManager>();

        Initialize(joystick.GetSkill(), _skillHolder);
        joystick.OnJoystickTrigger += JoystickTriggered;
    }

    private void Initialize(SkillBehavior skill, GameObject skillHolder)
    {
        
        _skill = skill;
        //_skillSound = GetComponent<AudioSource>();
        _skillSprite.sprite = _skill.skillSprite;
        //_skillMask.sprite = skill.skillSprite;
        _skillCooldownDuration = _skill.skillBaseCooldown;
        _skill.Initialize(_skillHolder);
        SkillReady();
    }

    // Update is called once per frame
    void Update()
    {
        bool skillCooldownComplete = (Time.time > _skillCooldownReadyTime);
        if (skillCooldownComplete)
        {
            SkillReady();
            if (joystickTriggered)
            {
                SkillTriggered();
            }
            joystickTriggered = false;
        }
        else
        {
            Cooldown();
            joystickTriggered = false;
        }
    }

    private void SkillReady()
    {
        skillCoolDownText.enabled = false;
        _skillMask.enabled = false;
        _skillMask.fillAmount = 1;
    }

    private void Cooldown()
    {
        _skillCooldownTimeLeft -= Time.deltaTime;
        float roundedCooldown = Mathf.Round(_skillCooldownTimeLeft);
        _skillMask.fillAmount = _skillCooldownTimeLeft/_skillCooldownDuration;
        skillCoolDownText.text = roundedCooldown.ToString();
    }

    private void SkillTriggered()
    {
        Debug.Log("Skill Triggered!!!");
        _skill.TriggerSkill(joystick);
        _skillCooldownReadyTime = _skillCooldownDuration + Time.time;
        _skillCooldownTimeLeft = _skillCooldownDuration;

        _skillMask.enabled = true;
        _skillMask.fillAmount = 1;
        skillCoolDownText.enabled = true;
        //_skillSound.clip = _skill.skillSound;
    }

    public void JoystickTriggered()
    {
        joystickTriggered = true;
    }
    public void JoystickNotTriggered()
    {
        joystickTriggered = false;
    }
}
