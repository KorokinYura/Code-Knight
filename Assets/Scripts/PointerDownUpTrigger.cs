using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PointerDownUpTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField]
    private Vector3 pointerDownScale;
    [SerializeField]
    private Color pointerDownColor;

    [SerializeField]
    private bool withSound = true;
    [SerializeField]
    private AudioSource buttonsAudioSource;
    [SerializeField]
    private AudioClip audioClip;

    private Vector3 prevScale;
    private Color prevColor;

    private Image image;
    private EventTrigger eventTrigger;

    public bool Clicked { get; private set; }

    private void Start()
    {
        image = GetComponent<Image>();
        eventTrigger = GetComponent<EventTrigger>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventTrigger != null)
            eventTrigger.enabled = true;

        if (withSound && Settings.SoundOn && buttonsAudioSource != null && audioClip != null)
        {
            buttonsAudioSource.clip = audioClip;
            buttonsAudioSource.Play();
        }

        prevScale = transform.localScale;
        transform.localScale = pointerDownScale;

        if (image != null)
        {
            prevColor = image.color;
            image.color = pointerDownColor;
        }
        Clicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = prevScale;

        if (image != null)
        {
            image.color = prevColor;
        }
        if (eventTrigger != null)
            eventTrigger.enabled = true;
        Clicked = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventTrigger != null && Clicked)
            eventTrigger.enabled = false;
        Clicked = false;
    }
}
