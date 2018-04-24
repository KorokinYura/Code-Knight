using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Door : MonoBehaviour, ITriggerable
{
    [SerializeField]
    private bool triggerableDirectly;
    public bool TriggerableDirectly { get { return triggerableDirectly; } }

    private bool triggered = false;
    public bool Triggered { get { return triggered; } }
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) Trigger();
    }

    public void Trigger()
    {
        triggered = !triggered;
        sr.flipY = triggered;
        transform.localEulerAngles += new Vector3(0, 0, triggered ? 90 : -90);
        tag = triggered ? "Untagged" : "Obstacle";
    }
}
