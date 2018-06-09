using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private GameObject playBtn;
    [SerializeField]
    private GameObject replayBtn;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private GameObject InfoTextBackground;
    [SerializeField]
    private Text infoText;

    public static PauseController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Win(int cmdsAmount)
    {
        gameObject.SetActive(true);
        nameText.text = LanguageController.Language.winName;
        InfoTextBackground.SetActive(true);
        infoText.text = string.Format(LanguageController.Language.winText, cmdsAmount);
        replayBtn.SetActive(true);
        playBtn.SetActive(false);
    }

    public void Pause()
    {
        gameObject.SetActive(true);
        nameText.text = LanguageController.Language.pauseName;
        InfoTextBackground.SetActive(false);
        replayBtn.SetActive(false);
        playBtn.SetActive(true);
    }
}
