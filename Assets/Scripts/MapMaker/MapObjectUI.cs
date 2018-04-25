using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MapMaker;

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
        GameObject obj = CreateMapObject(type);
        obj.GetComponent<BoxCollider2D>().enabled = false;
        DragObj = obj;
    }

    public static GameObject CreateMapObject(MapObjectType type)
    {
        GameObject obj = Instantiate(MapMakerController.Instance.MapObjectsPrefabs[(int)type], MapMakerController.Instance.Map.transform);

        Destroy(obj.GetComponent<Collider2D>());
        obj.AddComponent<MapObjectController>().Type = type;
        BoxCollider2D c = obj.AddComponent<BoxCollider2D>();
        c.isTrigger = true;

        return obj;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (MapObjectsDestroyer.PointerIsInside)
        {
            Destroy(DragObj);
        }
        else
        {
            DragObj.GetComponent<BoxCollider2D>().enabled = true;
            //DragObj.transform.position += new Vector3(0, 0, 9.5f);
        }
        DragObj = null;
    }
}
