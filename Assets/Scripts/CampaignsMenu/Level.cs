using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Level : MonoBehaviour, IPointerUpHandler
{
    [TextArea(5, 10)]
    [SerializeField]
    private string loadString;
    [Space]
    [SerializeField]
    private Image spriteTop;
    [SerializeField]
    private Image spriteBot;
    [SerializeField]
    private Color unavailableColor;
    [SerializeField]
    private Color availableColor;
    [Space]
    [SerializeField]
    private Text highScoreText;

    public int HighScore
    {
        get
        {
            int ret;
            int.TryParse(highScoreText.text, out ret);
            return ret;
        }
        set
        {
            highScoreText.text = value == 0 ? "-" : value.ToString();
        }
    }
    private bool available = false;
    public bool Available
    {
        get { return available; }
        set
        {
            available = value;
            spriteBot.color = spriteTop.color = value ? availableColor : unavailableColor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerDownUpTrigger trigger = GetComponent<PointerDownUpTrigger>();
        if (Available && (trigger == null || trigger.Clicked))
        {
            CampaignsMenuController.Instance.SetCurrentLevel(this);
            MapMakerController.CurLoadString = loadString;
            ScenesController.Instance.ToGameplay();
        }
    }
}
