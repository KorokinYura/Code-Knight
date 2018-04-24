using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBehaviour : MapObjectBehaviour
{
    protected override IEnumerator TicksCoroutine(float tickTime)
    {
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0.1f);
            if (hit.transform != null && hit.transform.gameObject.tag == "Player")
            {
                Debug.Log("AIM");
            }
            yield return new WaitForSeconds(tickTime);
        }
    }
}
