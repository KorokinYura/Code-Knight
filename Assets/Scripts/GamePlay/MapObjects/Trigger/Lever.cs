using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lever : MonoBehaviour, ITriggerable
{
    [SerializeField]
    private GameObject triggerObj;
    private ITriggerable trigger;

    [SerializeField]
    private SpriteRenderer wandSpriteRenderer;

    private LineRenderer lineRenderer;

    private void Start()
    {
        if (triggerObj != null)
        {
            trigger = triggerObj.GetComponent<ITriggerable>();
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetPositions(new Vector3[] {
                transform.position + new Vector3(0, 0, -0.001f),
                triggerObj.transform.position + new Vector3(0, 0, -0.001f)
            });
        }
    }

    public bool TriggerableDirectly { get { return true; } }

    private bool triggered = false;
    public bool Triggered { get { return triggered; } }

    public void Trigger()
    {
        triggered = !triggered;
        if(trigger != null)
            trigger.Trigger();
        if (wandSpriteRenderer != null)
            wandSpriteRenderer.flipY = triggered;
    }
}
