using System.Collections;
using UnityEngine;

public class Player : MapObjectBehaviour, IMortal
{
    protected override IEnumerator TicksCoroutine(float tickTime)
    {
        foreach (CommandUI c in GameController.Instance.GetCommandUIs())
        {
            yield return new WaitForSeconds(tickTime);
            c.CreateInstance(gameObject).Activate(0);
        }
        yield return new WaitForSeconds(tickTime);
        GameController.Instance.ResetLevel();
    }

    public void Die()
    {
        GameController.Instance.ResetLevel();
    }
}
