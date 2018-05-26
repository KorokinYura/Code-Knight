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
        // lang not implemented
    }

    public void TriggerMusic()
    {
        string[] settings = PlayerPrefs.GetString(settingsSaveName).Split(';');
        settings[0] = settings[0] == "1" ? "0" : "1";
        PlayerPrefs.SetString(settingsSaveName, string.Format("{0};{1};{2}", settings[0], settings[1], settings[2]));
        LoadSettings();
        GetComponent<AudioSource>().enabled = MusicOn;
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
        // lang not implemented
    }
}
