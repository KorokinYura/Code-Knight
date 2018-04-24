using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MapObjectBehaviour
{
    protected override IEnumerator TicksCoroutine(float tickTime)
    {
        List<CommandUI> cmds = new List<CommandUI>(GameController.Instance.GetCommandUIs());
        
        foreach (CommandUI c in GameController.Instance.GetCommandUIs())
        {
            yield return new WaitForSeconds(tickTime);
            c.CreateInstance(gameObject).Activate(0);
        }

        //GameController.Instance.ResetLevel();
    }

    public void Die()
    {
        GameController.Instance.ResetLevel();
    }
}
