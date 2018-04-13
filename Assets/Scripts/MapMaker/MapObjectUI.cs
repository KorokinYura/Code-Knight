using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapObjectUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static GameObject DragObj { get; private set; }

    [SerializeField]
    private MapObjectType type;

    private void Update()
    {
        if (DragObj != null)
        {
            Dragging();
        }
    }

    private void Dragging()
    {
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        DragObj.transform.position = new Vector3(Mathf.Round(mPos.x), Mathf.Round(mPos.y), Camera.main.transform.position.z + 0.5f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject obj = Instantiate(MapMakerController.Instance.MapObjectsPrefabs[(int)type], MapMakerController.Instance.Map.transform);
        DragObj = obj;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (MapObjectsDestroyer.PointerIsInside)
        {
            Destroy(DragObj);
        }
        else
        {
            DragObj.transform.position += new Vector3(0, 0, 9.5f);
        }
        DragObj = null;
    }

    private enum MapObjectType
    {
        Ground, Obstacle, Aim, Player
    }
}
