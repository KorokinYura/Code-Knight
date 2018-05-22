using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    private void Start()
    {
        GetComponent<AudioSource>().enabled = Settings.MusicOn;
    }
}
