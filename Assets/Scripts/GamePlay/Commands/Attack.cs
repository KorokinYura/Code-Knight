using System.Collections;
using UnityEngine;

public class Attack : Command
{
    public Attack(GameObject obj) : base(obj) { }

    public override bool Activate(float time)
    {
        RaycastHit2D hit = CheckFront(obj);
        if (hit.transform == null) return false;
        if (hit.transform.tag == "Player")
            hit.transform.GetComponent<Player>().Die();
        else if (hit.transform.tag == "Enemy")
            hit.transform.GetComponent<Enemy>().Die();

        return (hit.transform != null && (hit.transform.tag == "Player" || hit.transform.tag == "Enemy"));
    }
}
