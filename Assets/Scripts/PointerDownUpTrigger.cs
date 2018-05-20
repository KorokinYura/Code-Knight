﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PointerDownUpTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (withSound && buttonsAudioSource != null && audioClip != null)
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
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = prevScale;

        if (image != null)
        {
            image.color = prevColor;
        }
    }
}
