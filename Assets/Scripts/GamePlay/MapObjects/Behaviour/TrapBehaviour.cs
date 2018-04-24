﻿using System.Collections;
using UnityEngine;

public class TrapBehaviour : MapObjectBehaviour
{
    [SerializeField]
    [Range(2, 6)]
    private int ticksToTrigger;
    [SerializeField]
    private bool loopTicks;
    [SerializeField]
    private GameObject spikesObj;

    protected override void Start()
    {
        base.Start();
        spikesObj.SetActive(false);
    }

    protected override IEnumerator TicksCoroutine(float tickTime)
    {
        for(int i = 1; i <= ticksToTrigger; i++)
        {
            spikesObj.SetActive(false);
            if (i == ticksToTrigger)
            {
                if (loopTicks) i = 1;

                spikesObj.SetActive(true);

                RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + Vector2.up, Vector2.zero, 0.1f);
                if (hit.transform != null && hit.transform.gameObject.tag == "Player")
                {
                    hit.transform.gameObject.GetComponent<Player>().Die();
                }
            }
            yield return new WaitForSeconds(tickTime);
        }
    }
}
