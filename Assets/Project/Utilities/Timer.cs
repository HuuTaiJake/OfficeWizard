using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoSingleton<Timer>
{
    public static int timer = 0;
    public UnityAction TimerTick;
    IEnumerator Start()
    {
        while (true)
        {
            timer++;
            TimerTick?.Invoke();
            yield return new WaitForSeconds(1f);
        }
    }
}