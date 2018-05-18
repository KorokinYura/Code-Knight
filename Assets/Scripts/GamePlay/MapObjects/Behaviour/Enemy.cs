using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MapObjectBehaviour, IMortal
{
    public Command[] Cmds { get; private set; }

    public bool Blocked { get { return false; } }
    public bool IsDead { get; private set; }

    private bool attacked = false;

    public void SetCommands(List<Command> commands)
    {
        if (commands != null)
        {
            Cmds = new Command[commands.Count];
            commands.CopyTo(Cmds);
        }
    }

    protected override IEnumerator TicksCoroutine(float tickTime)
    {
        int prevAngleZ = 0;
        int playerAngleZ = 0;
        if (Cmds == null || Cmds.Length == 0) Cmds = new Command[]{ new Wait(gameObject) };
        while (!IsDead)
        {
            foreach (Command c in Cmds)
            {
                if (attacked) attacked = false;
                else
                {
                    RaycastHit2D hit;
                    hit = Physics2D.Raycast(transform.position + (Vector3)Vector2.up, Vector2.zero, 1);
                    playerAngleZ = 0;
                    if (hit.transform == null || hit.transform.tag != "Player")
                    {
                        hit = Physics2D.Raycast(transform.position + (Vector3)Vector2.right, Vector2.zero, 1);
                        playerAngleZ = 270;
                    }
                    if (hit.transform == null || hit.transform.tag != "Player")
                    {
                        hit = Physics2D.Raycast(transform.position + (Vector3)Vector2.down, Vector2.zero, 1);
                        playerAngleZ = 180;
                    }
                    if (hit.transform == null || hit.transform.tag != "Player")
                    {
                        hit = Physics2D.Raycast(transform.position + (Vector3)Vector2.left, Vector2.zero, 1);
                        playerAngleZ = 90;
                    }
                    if(hit.transform == null || hit.transform.tag != "Player")
                    {
                        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero, 1);
                        foreach(RaycastHit2D h in hits)
                        {
                            if (h.transform != null && h.transform.tag == "Player")
                            {
                                hit = h;
                                playerAngleZ = (int)h.transform.localEulerAngles.z;
                                yield return new WaitForSeconds(tickTime);
                                break;
                            }
                        }
                    }
                    if (hit.transform != null && hit.transform.tag == "Player")
                    {
                        prevAngleZ = (int)transform.localEulerAngles.z;
                        transform.localEulerAngles = new Vector3(0, 0, playerAngleZ);
                        yield return new WaitForSeconds(tickTime - Time.deltaTime);
                        new Attack(gameObject).Activate(0);
                        yield return new WaitForSeconds(Time.deltaTime);
                        attacked = true;

                        transform.localEulerAngles = new Vector3(0, 0, prevAngleZ);
                    }
                }
                yield return new WaitForSeconds(tickTime);
                c.Activate(0);
            }
        }
    }

    public void Die()
    {
        StartCoroutine(DieCoroutine(TickController.TickTime / 2));
        IsDead = true;
    }
    private IEnumerator DieCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public void Block()
    {
        Debug.Log("ONLY PLAYER CAN BLOCK");
    }
}