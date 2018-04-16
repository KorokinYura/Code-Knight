using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForward : Command
{
    public GoForward(GameObject player) : base(player) { }

    public override bool Use(float time = 0)
    {
        RaycastHit2D hit = Physics2D.Raycast(obj.transform.right * 0.55f + obj.transform.position, obj.transform.right, 0.9f);
        if (hit.transform != null && hit.transform.gameObject.tag == "Obstacle")
        {
            return false;
        }
        obj.transform.position += obj.transform.right;
        return true;
    }
}
