using UnityEngine;
using UnityEngine.UI;

public class LangMainMenu : MonoBehaviour
{
    [SerializeField]
    private Text infoPnl;

    private void Awake()
    {
        LanguageController.ChangeLanguage += Translate;
    }

    public void Translate()
    {
        infoPnl.text = LanguageController.Language.info;
    }

    private void OnDestroy()
    {
        LanguageController.ChangeLanguage -= Translate;
    }
}
