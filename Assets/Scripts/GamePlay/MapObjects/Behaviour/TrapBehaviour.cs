using System.Collections;
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

                RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up, Vector2.zero, 1);
                if (hit.transform != null && hit.transform.GetComponent<IMortal>() != null)
                {
                    hit.transform.GetComponent<IMortal>().Die();
                }
            }
            yield return new WaitForSeconds(tickTime);
        }
    }
}
