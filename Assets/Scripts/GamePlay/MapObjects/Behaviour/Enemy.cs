using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MapObjectBehaviour, IMortal
{
    public Command[] Cmds { get; private set; }
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
        while (true)
        {
            foreach (Command c in Cmds)
            {
                if (attacked) attacked = false;
                else
                {
                    RaycastHit2D hit;
                    hit = Physics2D.Raycast(transform.position, Vector2.up, 1);
                    playerAngleZ = 0;
                    if (hit.transform == null || hit.transform.tag != "Player")
                    {
                        hit = Physics2D.Raycast(transform.position, Vector2.right, 1);
                        playerAngleZ = 270;
                    }
                    if (hit.transform == null || hit.transform.tag != "Player")
                    {
                        hit = Physics2D.Raycast(transform.position, Vector2.down, 1);
                        playerAngleZ = 180;
                    }
                    if (hit.transform == null || hit.transform.tag != "Player")
                    {
                        hit = Physics2D.Raycast(transform.position, Vector2.left, 1);
                        playerAngleZ = 90;
                    }
                    if (hit.transform != null && hit.transform.tag == "Player")
                    {
                        prevAngleZ = (int)transform.localEulerAngles.z;
                        transform.localEulerAngles = new Vector3(0, 0, playerAngleZ);
                        new Attack(gameObject).Activate(0);

                        yield return new WaitForSeconds(tickTime);
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
        Destroy(gameObject);
    }
}