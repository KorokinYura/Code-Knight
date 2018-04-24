using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use : Command
{
    public Use(GameObject player) : base(player) { }

    public override bool Activate(float time)
    {
        RaycastHit2D hit = CheckFront(obj);
        if (hit.transform == null) return false;
        ITriggerable t = hit.transform.GetComponent<ITriggerable>();
        if (t == null || !t.TriggerableDirectly) return false;
        t.Trigger();
        return true;
    }
}
