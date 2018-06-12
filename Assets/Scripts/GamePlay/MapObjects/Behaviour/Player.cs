using System.Collections;
using UnityEngine;

public class Player : MapObjectBehaviour, IMortal
{
    public bool IsDead { get; private set; }

    public bool Blocked { get; private set; }

    protected override IEnumerator TicksCoroutine(float tickTime)
    {
        foreach (CommandUI c in GameController.Instance.GetCommandUIs())
        {
            yield return new WaitForSeconds(tickTime);
            if(!IsDead) c.CreateInstance(gameObject).Activate(0);
        }
        yield return new WaitForSeconds(tickTime);
        GameController.Instance.ResetLevel();
    }

    public void Die()
    {
        StartCoroutine(DieCoroutine(TickController.TickTime * 2));
        IsDead = true;
        GetComponent<Animator>().SetTrigger("Death");
    }
    private IEnumerator DieCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        //GameController.Instance.ResetLevel();
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public void Block()
    {
        Blocked = true;
        StartCoroutine(BlockCoroutine());
    }
    private IEnumerator BlockCoroutine()
    {
        yield return new WaitForSeconds(TickController.TickTime * 3);
        Blocked = false;
    }
}
