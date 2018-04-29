using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class CommandUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static CommandUI DragCmd { get; private set; }

    [SerializeField]
    private Command.Type type;
    public Command.Type Type { get { return type; } }

    private bool isDragging = false;

    private bool pointerIsInside = false;

    private void Update()
    {
        Dragging();
    }

    private void Dragging()
    {
        if (isDragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = (Vector2)transform.localPosition;
        }
    }

    public Command CreateInstance(GameObject go)
    {
        return Command.FromEnum(type, go);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(transform.parent.gameObject == GameController.Instance.CommandsPool)
        {
            GameObject cmd = Instantiate(gameObject, GameController.Instance.CommandsDrag.transform);
            (cmd.transform as RectTransform).sizeDelta = (transform as RectTransform).sizeDelta;
            
            StartDrag(cmd.GetComponent<CommandUI>());
        }
        else if(transform.parent.gameObject == GameController.Instance.CurCommandsList)
        {
            transform.SetParent(GameController.Instance.CommandsDrag.transform);

            StartDrag(this);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (DragCmd != null && DragCmd.isDragging)
        {
            if (CommandsList.PointerIsInside)
            {
                DragCmd.transform.SetParent(GameController.Instance.CurCommandsList.transform);
                DragCmd.transform.SetSiblingIndex(GameController.Instance.CurCommandsList.GetComponent<CommandsList>().CmdPlaceHolderIndex);

                EndDrag(DragCmd);
                DragCmd = null;
            }
            else if(pointerIsInside)
            {
                DragCmd.transform.SetParent(GameController.Instance.CurCommandsList.transform);

                EndDrag(DragCmd);
                DragCmd = null;
            }
            else
            {
                Destroy(DragCmd.gameObject);
            }
        }
    }

    private void StartDrag(CommandUI dragCmd)
    {
        DragCmd = dragCmd;
        DragCmd.isDragging = true;
        DragCmd.Dragging();
        DragCmd.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    private void EndDrag(CommandUI dragCmd)
    {
        DragCmd.isDragging = false;
        DragCmd.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerIsInside = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        pointerIsInside = false;
    }
}
