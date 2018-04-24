using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerable
{
    bool TriggerableDirectly { get; }
    bool Triggered { get; }

    void Trigger();
}
