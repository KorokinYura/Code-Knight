using System.Collections;
using UnityEngine;

public class AimBehaviour : MapObjectBehaviour
{
    protected override IEnumerator TicksCoroutine(float tickTime)
    {
        while (true)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero, 0.1f);
            foreach(RaycastHit2D hit in hits)
            {
                if(hit.transform != null && hit.transform.tag == "Player")
                {
                    CampaignsMenuController.SetCurrentLevelScore(GameController.Instance.CommandsAmount);
                    yield return new WaitForSeconds(tickTime / 2);
                    PauseController.Instance.Win(GameController.Instance.CommandsAmount);
                }
            }
            yield return new WaitForSeconds(tickTime);
        }
    }
}
