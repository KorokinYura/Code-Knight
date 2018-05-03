using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Command
{
    public Block(GameObject obj) : base(obj) { }

    public override bool Activate(float time)
    {
        IMortal m = obj.GetComponent<IMortal>();
        if (m == null) return false;
        m.Block();
        return true;
    }
}
