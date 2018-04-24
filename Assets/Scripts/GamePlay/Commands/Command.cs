using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    protected GameObject obj;

    public Command(GameObject obj)
    {
        this.obj = obj;
    }

    protected static RaycastHit2D CheckFront(GameObject obj)
    {
        return Physics2D.Raycast(obj.transform.position, obj.transform.up, 1);
    }

    public abstract bool Activate(float time);

    public enum Type
    {
        GoForward, TurnLeft, TurnRight, Wait, Use, Func1, Func2
    }
}
