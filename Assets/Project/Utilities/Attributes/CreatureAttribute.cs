using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum StatusDefine
{
    Normal,
    Slow,
    Burn
}
public class CreatureAttribute : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float speed;
    private float _maxSpeed;

    public float defense;
    private float _maxDefense;


    [SerializeField] private int _damagePerSecs;
    private bool[] _isStatus;
    private Coroutine[] _statusCoroutine;


    public virtual void Start()
    {
        currentHealth = maxHealth;
        _maxSpeed = speed;
        _maxDefense = defense;
        _isStatus = new bool[Enum.GetNames(typeof(StatusDefine)).Length];
        _statusCoroutine = new Coroutine[Enum.GetNames(typeof(StatusDefine)).Length];

        StartCoroutine(StatusTimer(0.5f));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetStatus(StatusDefine.Burn, 10f);
        }
        if(currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    private void StatusProcess(int status)
    {
        switch (status)
        {
            case (int)StatusDefine.Normal:
                break;
            case (int)StatusDefine.Burn:
                TakeNormalDamage(_damagePerSecs);
                Debug.Log(currentHealth);
                break;
        }

        if (status == (int)StatusDefine.Slow)
        {
            speed = _maxSpeed / 2;
        }
        else
        {
            speed = _maxSpeed;
        }

    }

    public void SetStatus(StatusDefine status, float duration)
    {
        if(_statusCoroutine[(int)status] != null)
        {
            StopCoroutine(_statusCoroutine[(int)status]);
            _statusCoroutine[(int)status] = null;
            _statusCoroutine[(int)status] = StartCoroutine(StatusTrigger(status, duration));
        }
        else
        {
            _statusCoroutine[(int)status] = StartCoroutine(StatusTrigger(status, duration));
        }
    }

    IEnumerator StatusTrigger(StatusDefine status, float duration)
    {
        _isStatus[(int)status] = true;
        yield return new WaitForSeconds(duration);
        _isStatus[(int)status] = false;
    }

    IEnumerator StatusTimer(float duration)
    {
        while (true)
        {
            yield return new WaitForSeconds(duration);
            for (int status = 0; status < Enum.GetNames(typeof(StatusDefine)).Length; status++)
            {
                if (_isStatus[status])
                {
                    StatusProcess(status);
                }
            }
        }
    }

    public bool GetStatus(StatusDefine status)
    {
        return _isStatus[(int)status];
    }

    public void TakePiercingDamage(int dmgToTake)
    {
        currentHealth -= dmgToTake;
    }

    public virtual void TakeNormalDamage (int dmgToTake)
    {
        if (defense >= 0)
        {
            currentHealth -= (dmgToTake - (int)defense) <= 0 ? 0 : (dmgToTake - (int)defense);
        }
        else
        {
            currentHealth -= 1;
        }
    }
}