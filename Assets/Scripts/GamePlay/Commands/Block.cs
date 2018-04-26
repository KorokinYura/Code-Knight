using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Command
{
    public Block(GameObject obj) : base(obj) { }

    public override bool Activate(float time)
    {
        return false;
    }
}
