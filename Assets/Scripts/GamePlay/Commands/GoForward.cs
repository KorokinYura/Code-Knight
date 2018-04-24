using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForward : Command
{
    public GoForward(GameObject player) : base(player) { }

    public override bool Activate(float time = 0)
    {
        RaycastHit2D hit = CheckFront(obj);
        if (hit.transform != null && hit.transform.gameObject.tag == "Obstacle")
        {
            return false;
        }
        obj.transform.position += obj.transform.up;
        return true;
    }
}
