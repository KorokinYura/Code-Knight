using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Command
{
    public Attack(GameObject obj) : base(obj) { }

    public override bool Activate(float time)
    {
        return false;
    }
}
