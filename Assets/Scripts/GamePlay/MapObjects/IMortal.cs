using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMortal
{
    bool Blocked { get; }
    void Block();
    
    bool IsDead { get; }
    void Die();
}
