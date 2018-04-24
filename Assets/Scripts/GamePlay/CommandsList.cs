using UnityEngine;
using UnityEngine.EventSystems;

public class CommandsList : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool PointerIsInside { get; private set; }
    
    [SerializeField]
    private GameObject cmdPlaceHolder;
    public int CmdPlaceHolderIndex { get; private set; }

    private void Start()
    {
        cmdPlaceHolder = Instantiate(cmdPlaceHolder, transform);
        cmdPlaceHolder.SetActive(false);
    }

    private void Update()
    {
        PlaceCmdPlaceHolder();
    }

    private void PlaceCmdPlaceHolder()
    {
        if (CommandUI.DragCmd != null && PointerIsInside)
        {
            cmdPlaceHolder.SetActive(true);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Transform nearestCmd = transform.GetChild(0);
            int nearestCmdIndex = 0;
            
            for(int i = 0; i < transform.childCount; i++)
            {
                Transform t = transform.GetChild(i);
                if (Vector2.Distance(mousePos, nearestCmd.position) > Vector2.Distance(mousePos, t.position))
                {
                    nearestCmd = t;
                    nearestCmdIndex = i;
                }
            }

            cmdPlaceHolder.transform.SetSiblingIndex(nearestCmdIndex);
            CmdPlaceHolderIndex = nearestCmdIndex;
        }
        else
        {
            cmdPlaceHolder.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerIsInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerIsInside = false;
    }
}
