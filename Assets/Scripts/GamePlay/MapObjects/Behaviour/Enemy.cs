using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MapObjectBehaviour
{
    protected override IEnumerator TicksCoroutine(float tickTime)
    {
        yield return new WaitForSeconds(tickTime);
    }
}
