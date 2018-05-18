using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLeft : Command
{
    public TurnLeft(GameObject obj) : base(obj) { }

    public override bool Activate(float time = 0)
    {
        if (obj == null) return false;
        obj.transform.localEulerAngles += new Vector3(0, 0, 90);

        return true;
    }
}
