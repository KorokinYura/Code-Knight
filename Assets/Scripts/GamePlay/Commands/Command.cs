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

    public static Command FromEnum(Type type, GameObject go)
    {
        switch (type)
        {
            case Type.GoForward:
                return new GoForward(go);
            case Type.TurnLeft:
                return new TurnLeft(go);
            case Type.TurnRight:
                return new TurnRight(go);
            case Type.Wait:
                return new Wait(go);
            case Type.Use:
                return new Use(go);
            case Type.Attack:
                return new Attack(go);
            case Type.Block:
                return new Block(go);
            default:
                return null;
        }
    }

    public enum Type
    {
        GoForward, TurnLeft, TurnRight, Wait, Use, Func1, Func2, Attack, Block
    }
}
