using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lever : MonoBehaviour, ITriggerable
{
    [SerializeField]
    private GameObject triggerObj;
    public GameObject TriggerObj { get { return triggerObj; } set { triggerObj = value; } }
    private ITriggerable trigger;

    [SerializeField]
    private SpriteRenderer wandSpriteRenderer;

    private LineRenderer lineRenderer;


    public bool TriggerableDirectly { get { return true; } }

    private bool triggered = false;
    public bool Triggered { get { return triggered; } }

    private void Start()
    {
        if (triggerObj != null)
        {
            trigger = triggerObj.GetComponent<ITriggerable>();
            lineRenderer = GetComponent<LineRenderer>();
        }
    }

    private void Update()
    {
        if(trigger != null)
        {
            lineRenderer.SetPositions(new Vector3[] {
                transform.position + new Vector3(0, 0, -0.001f),
                triggerObj.transform.position + new Vector3(0, 0, -0.001f)
            });
        }
    }

    public void Trigger()
    {
        triggered = !triggered;
        if (trigger != null)
            trigger.Trigger();
        if (wandSpriteRenderer != null)
            wandSpriteRenderer.flipY = triggered;
    }
}
