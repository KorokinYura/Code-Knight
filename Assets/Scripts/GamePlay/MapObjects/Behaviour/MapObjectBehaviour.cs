using System.Collections;
using UnityEngine;

public abstract class MapObjectBehaviour : MonoBehaviour
{
    protected abstract IEnumerator TicksCoroutine(float tickTime);

    protected virtual void Start()
    {
        TickController.TicksEvent += TicksCoroutine;
    }

    protected virtual void OnDestroy()
    {
        TickController.TicksEvent -= TicksCoroutine;
    }
}
