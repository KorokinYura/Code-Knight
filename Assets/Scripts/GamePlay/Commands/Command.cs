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

    public abstract bool Use(float time);

    public enum Type
    {
        GoForward, TurnLeft, TurnRight, Wait, Func1, Func2
    }
}
