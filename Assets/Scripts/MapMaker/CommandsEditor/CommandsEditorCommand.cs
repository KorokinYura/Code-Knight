using UnityEngine;
using UnityEngine.EventSystems;

public class CommandsEditorCommand : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private CmdType type;
    public CmdType Type { get { return type; } }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(transform.parent == CommandsEditor.Instance.CommandsPool.transform)
        {
            Instantiate(gameObject, CommandsEditor.Instance.CommandsList.transform);
        }
        else if(transform.parent == CommandsEditor.Instance.CommandsList.transform)
        {
            Destroy(gameObject);
        }
    }

    public enum CmdType
    {
        GoForward, Left, Right, Use
    }
}
