using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    private static float time = 0;

    private AudioSource audioSource;

    public static MusicController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Play();
    }
    
    public void Play()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.time = time;
        //audio.enabled = Settings.MusicOn;
        if (Settings.MusicOn) audioSource.Play();
    }

    private void Update()
    {
        audioSource = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            time = 0;
        }
        else
        {
            time = audioSource.time;
        }
    }
}
