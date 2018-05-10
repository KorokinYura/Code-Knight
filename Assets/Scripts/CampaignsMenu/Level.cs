using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level : MonoBehaviour, IPointerUpHandler
{
    [TextArea()]
    [SerializeField]
    private string loadString;

    private void Start()
    {
        MapMakerController.CurLoadString = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MapMakerController.CurLoadString = loadString;
    }
}
