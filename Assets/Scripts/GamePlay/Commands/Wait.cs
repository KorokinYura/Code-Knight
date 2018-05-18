﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : Command
{
    public Wait(GameObject obj) : base(obj) { }

    public override bool Activate(float time = 0)
    {
        if (obj == null) return false;

        return true;
    }
}
