using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapObjects;

public class MapObjectController : MonoBehaviour
{
    private const float additionalActionTapTime = 1.0f;
    private Coroutine additionalActionTapCoroutine;

    private bool isDragging = false;
    private bool moved = false;
    public яяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяя Type { get; set; }

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
        int newX = (int)Mathf.Round(mPos.x), newY = (int)Mathf.Round(mPos.y);
        if ((int)Mathf.Round(mPos.x) != (int)Mathf.Round(transform.position.x) ||
            (int)Mathf.Round(mPos.y) != (int)Mathf.Round(transform.position.y))
            moved = true;
        transform.position = new Vector3(newX, newY, Camera.main.transform.position.z + 0.5f);
    }

    private void OnMouseDown()
    {
        isDragging = true;
        GetComponent<BoxCollider2D>().enabled = false;
        CameraController.CanControlCamera = false;

        additionalActionTapCoroutine = StartCoroutine(AdditionalActionTapCoroutine());
    }

    private void OnMouseUp()
    {
        if(additionalActionTapCoroutine != null)
        {
            StopCoroutine(additionalActionTapCoroutine);
        }

        if (MapObjectsDestroyer.PointerIsInside)
        {
            Destroy(gameObject);
        }
        isDragging = false;
        if (!moved) transform.localEulerAngles -= new Vector3(0, 0, 90);
        moved = false;
        GetComponent<BoxCollider2D>().enabled = true;
        CameraController.CanControlCamera = true;
    }

    private IEnumerator AdditionalActionTapCoroutine()
    {
        yield return new WaitForSeconds(additionalActionTapTime);
        if(!moved)
        {
            ITriggerable t = GetComponent<ITriggerable>();
            if(t != null)
            {
                GameObject obj = MapObjectUI.CreateMapObject(яяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяяя.Lever);
                obj.transform.position = transform.position;
                obj.GetComponent<Lever>().TriggerObj = gameObject;
            }
            Enemy e = GetComponent<Enemy>();
            if(e != null)
            {
                MapMakerController.Instance.MapObjectCommandsEditor.CommandsEditorAim = e;
            }
            moved = true;
        }
    }
}
