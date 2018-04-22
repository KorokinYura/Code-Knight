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

    public void StartTicks()
    {
        foreach(Delegate d in TicksEvent.GetInvocationList())
        {
            Debug.Log("ADD");
            ticksCoroutines.Add(StartCoroutine((IEnumerator)d.DynamicInvoke(tickTime)));
        }
    }

    public void StopTicks()
    {
        foreach (Coroutine c in ticksCoroutines)
        {
            StopCoroutine(c);
        }
        ticksCoroutines = new List<Coroutine>();
    }

    public delegate IEnumerator TicksDelegate(float tickTime);
}
