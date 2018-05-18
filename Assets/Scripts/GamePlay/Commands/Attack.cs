using UnityEngine;

public class Attack : Command
{
    public Attack(GameObject obj) : base(obj) { }

    public override bool Activate(float time)
    {
        if (obj == null) return false;

        RaycastHit2D hit = CheckFront(obj);
        if (hit.transform == null) return false;
        IMortal m = hit.transform.GetComponent<IMortal>();
        if (m == null || m.Blocked) return false;

        m.Die();
        return true;
    }
}
