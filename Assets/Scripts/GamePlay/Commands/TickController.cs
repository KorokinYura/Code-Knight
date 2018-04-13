using System.Collections;
using UnityEngine;

public class TickController : MonoBehaviour
{
    public static event TicksDelegate TicksEvent;
    private Coroutine ticksCoroutine;
    [SerializeField]
    private float tickTime;

    private bool isTicking = false;

    public void StartTicks()
    {
        if (TicksEvent != null)
        {
            ticksCoroutine = StartCoroutine(TicksEvent(tickTime));
        }
    }

    public void StopTicks()
    {
        StopCoroutine(ticksCoroutine);
    }
    
    public delegate IEnumerator TicksDelegate(float tickTime);
}
