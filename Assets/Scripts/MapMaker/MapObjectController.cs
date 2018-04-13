using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapMaker;

public class MapObjectController : MonoBehaviour
{
    private bool isDragging;
    public MapObjectType Type { get; set; }

    private void Update()
    {
        if (isDragging)
        {
            Dragging();
        }
    }

    private void Dragging()
    {
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(Mathf.Round(mPos.x), Mathf.Round(mPos.y), Camera.main.transform.position.z + 0.5f);
    }

    private void OnMouseDown()
    {
        isDragging = true;
        GetComponent<BoxCollider2D>().enabled = false;
        CameraController.CanControlCamera = false;
    }

    private void OnMouseUp()
    {
        if (MapObjectsDestroyer.PointerIsInside)
        {
            Destroy(gameObject);
        }
        isDragging = false;
        GetComponent<BoxCollider2D>().enabled = true;
        CameraController.CanControlCamera = true;
    }
}
