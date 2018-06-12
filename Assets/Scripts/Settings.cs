using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Settings : MonoBehaviour
{
    private const string settingsSaveName = "Settings";

    [SerializeField]
    private Image musicImage;
    [SerializeField]
    private Sprite musicOn;
    [SerializeField]
    private Sprite musicOff;
    [Space]
    [SerializeField]
    private Image soundImage;
    [SerializeField]
    private Sprite soundOn;
    [SerializeField]
    private Sprite soundOff;
    [Space]
    [SerializeField]
    private Image languageImage;
    [SerializeField]
    private Sprite engFlag;
    [SerializeField]
    private Sprite rusFlag;
    [SerializeField]
    private Sprite ukrFlag;

    public static bool MusicOn { get; private set; }
    public static bool SoundOn { get; private set; }

    private void Awake()
    {
        LoadSettings();
    }

    private void LoadSettings() // Settings string: musicOn;soundOn;language     ex: 0;1;eng
    {
        string settingsString = PlayerPrefs.GetString(settingsSaveName);
        if(settingsString == string.Empty)
        {
            settingsString = "1;1;eng";
            PlayerPrefs.SetString(settingsSaveName, settingsString);
        }
        string[] settings = settingsString.Split(';');
        if (settings[0] == "0")
        {
            MusicOn = false;
            musicImage.sprite = musicOff;
        }
        else if (settings[0] == "1")
        {
            MusicOn = true;
            musicImage.sprite = musicOn;
        }
        if (settings[1] == "0")
        {
            SoundOn = false;
            soundImage.sprite = soundOff;
        }
        else if (settings[1] == "1")
        {
            SoundOn = true;
            soundImage.sprite = soundOn;
        }
        // langs: eng, rus, ukr
        if(settings[2] == "eng")
        {
            languageImage.sprite = engFlag;
            LanguageController.LangType = LanguageController.Lang.ENG;
        }
        else if(settings[2] == "rus")
        {
            languageImage.sprite = rusFlag;
            LanguageController.LangType = LanguageController.Lang.RUS;
        }
        else if(settings[2] == "ukr")
        {
            languageImage.sprite = ukrFlag;
            LanguageController.LangType = LanguageController.Lang.UKR;
        }
        LanguageController.LangLoad();
    }

    public void TriggerMusic()
    {
        string[] settings = PlayerPrefs.GetString(settingsSaveName).Split(';');
        settings[0] = settings[0] == "1" ? "0" : "1";
        PlayerPrefs.SetString(settingsSaveName, string.Format("{0};{1};{2}", settings[0], settings[1], settings[2]));
        LoadSettings();
        GetComponent<AudioSource>().enabled = MusicOn;
        MusicController.Instance.Play();
    }
    public void TriggerSound()
    {
        string[] settings = PlayerPrefs.GetString(settingsSaveName).Split(';');
        settings[1] = settings[1] == "1" ? "0" : "1";
        PlayerPrefs.SetString(settingsSaveName, string.Format("{0};{1};{2}", settings[0], settings[1], settings[2]));
        LoadSettings();
    }
    public void TriggerLanguage()
    {
        string[] settings = PlayerPrefs.GetString(settingsSaveName).Split(';');
        settings[2] = settings[2] == "eng" ? "rus" : settings[2] == "rus" ? "ukr" : "eng";
        PlayerPrefs.SetString(settingsSaveName, string.Format("{0};{1};{2}", settings[0], settings[1], settings[2]));
        LoadSettings();
    }
}
