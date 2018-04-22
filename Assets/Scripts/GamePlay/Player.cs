using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Command> commands = new List<Command>();

    private void Start()
    {
        TickController.TicksEvent += CommandsCoroutine;
    }

    private IEnumerator CommandsCoroutine(float tickTime)
    {
        List<CommandUI> cmds = new List<CommandUI>(GameController.Instance.GetCommandUIs());
        
        foreach (CommandUI c in GameController.Instance.GetCommandUIs())
        {
            Debug.Log(123);
            yield return new WaitForSeconds(tickTime);
            c.CreateInstance(gameObject).Use(0);
        }
        commands = new List<Command>();
    }

    //private GameObject winText;

    //private void CheckAim()
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0.1f);
    //    if(hit.transform != null && hit.transform.gameObject.tag == "Aim")
    //    {
    //        winText.SetActive(true);
    //    }
    //}
}
