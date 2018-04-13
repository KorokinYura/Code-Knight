using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRight : Command
{
    public TurnRight(GameObject player) : base(player) { }

    public override bool Use(float time = 0)
    {
        player.transform.localEulerAngles -= new Vector3(0, 0, 90);

        return true;
    }
}
