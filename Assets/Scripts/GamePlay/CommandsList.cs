using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CommandsList : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool PointerIsInside { get; private set; }
    
    [SerializeField]
    private GameObject cmdPlaceHolder;
    public int CmdPlaceHolderIndex { get; private set; }

    [SerializeField]
    private Text cmdsAmountText;
    [SerializeField]
    private int maxCmdsAmount;
    [SerializeField]
    private string listName;

    private void Start()
    {
        cmdPlaceHolder = Instantiate(cmdPlaceHolder, transform);
        cmdPlaceHolder.SetActive(false);
    }

    private void Update()
    {
        PlaceCmdPlaceHolder();
        UpdateCmdsAmountText();
    }

    private void PlaceCmdPlaceHolder()
    {
        if (CommandUI.DragCmd != null && PointerIsInside && transform.childCount <= maxCmdsAmount)
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

    private void UpdateCmdsAmountText()
    {
        cmdsAmountText.text = listName + " " + (transform.childCount - 1) + "/" + maxCmdsAmount;
    }

    public bool InsertChild(Transform t, bool toTheEnd)
    {
        if (transform.childCount - 1 >= maxCmdsAmount) return false;
        t.SetParent(transform);
        if (!toTheEnd) t.SetSiblingIndex(CmdPlaceHolderIndex);
        return true;
    }

    private void OnEnable()
    {
        cmdsAmountText.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        cmdsAmountText.gameObject.SetActive(false);
    }
}
