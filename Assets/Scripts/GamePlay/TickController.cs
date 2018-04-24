using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TickController : MonoBehaviour
{
    public static event TicksDelegate TicksEvent;
    private List<Coroutine> ticksCoroutines = new List<Coroutine>();
    [SerializeField]
    private float tickTime;

    private void Awake()
    {
        TicksEvent += TicksCounter;
    }

    public void StartTicks()
    {
        foreach(Delegate d in TicksEvent.GetInvocationList())
        {
            ticksCoroutines.Add(StartCoroutine((IEnumerator)d.DynamicInvoke(tickTime)));
        }
    }

    public void StopTicks()
    {
        foreach (Coroutine c in ticksCoroutines)
        {
            if(c != null)
                StopCoroutine(c);
        }
        ticksCoroutines = new List<Coroutine>();
    }

    private IEnumerator TicksCounter(float tickTime)
    {
        for(int i = 0; i < GameController.Instance.MaxCommandsAmount; i++)
        {
            yield return new WaitForSeconds(tickTime);
        }
        GameController.Instance.ResetLevel();
    }

    public delegate IEnumerator TicksDelegate(float tickTime);
}
