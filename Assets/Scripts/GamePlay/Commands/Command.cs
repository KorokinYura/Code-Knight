using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    protected GameObject player;

    public Command(GameObject player)
    {
        this.player = player;
    }

    public abstract bool Use(float time);

    public enum Type
    {
        GoForward, TurnLeft, TurnRight
    }
}
