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
            Debug.Log(hit.transform);
            if (hit.transform != null && hit.transform.gameObject.tag == "Player")
            {
                Debug.Log(123);
                // TODO: implement it blin and delete chto tap napisano
                GameObject.Find("ScenesController").GetComponent<ScenesController>().ToCampaignsMenu();
                // TODO
            }
            yield return new WaitForSeconds(tickTime);
        }
    }
}
