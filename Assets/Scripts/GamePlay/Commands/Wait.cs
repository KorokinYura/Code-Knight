using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : Command
{
    public Wait(GameObject obj) : base(obj) { }

    public override bool Use(float time = 0)
    {

        return true;
    }
}
