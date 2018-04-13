using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapObjectsDestroyer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool PointerIsInside { get; private set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerIsInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerIsInside = false;
    }
}
