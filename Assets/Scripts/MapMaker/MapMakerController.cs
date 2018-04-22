using System;
using UnityEngine;
using MapMaker;

public class MapMakerController : MonoBehaviour
{
    private static string Clipboard
    {
        get { return GUIUtility.systemCopyBuffer; }
        set { GUIUtility.systemCopyBuffer = value; }
    }

    [SerializeField]
    private GameObject map;
    public GameObject Map { get { return map; } }

    [SerializeField]
    private GameObject[] mapObjectsPrefabs;
    public GameObject[] MapObjectsPrefabs { get { return mapObjectsPrefabs; } }

    public static MapMakerController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SaveMap()
    {
        string saveStr = "";
        for(int i = 0; i < map.transform.childCount; i++)
        {
            GameObject obj = map.transform.GetChild(i).gameObject;
            saveStr += obj.GetComponent<MapObjectController>().Type + "," 
                + Mathf.Round(obj.transform.position.x) + ","
                + Mathf.Round(obj.transform.position.y);
            if(i != map.transform.childCount - 1)
            {
                saveStr += "|";
            }
        }
        Clipboard = saveStr;
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A)) SaveMap();
        if (Input.GetKeyDown(KeyCode.D)) LoadMap(true);
    }

    public void LoadMap(bool forMapMaker = false)
    {
        ClearMap();
        
        string[] strs = Clipboard.Split('|');
        for(int i = 0; i < strs.Length; i++)
        {
            try
            {
                string[] s = strs[i].Split(',');
                MapObjectType type = (MapObjectType)Enum.Parse(typeof(MapObjectType), s[0]);

                GameObject obj;
                if (forMapMaker) obj = MapObjectUI.CreateMapMakerCommand(type);
                else obj = Instantiate(MapObjectsPrefabs[(int)type], map.transform);

                obj.transform.position = new Vector2(
                    Int32.Parse(s[1]),
                    Int32.Parse(s[2])
                    );
            }
            catch
            {
                return;
            }
        }
    }

    public void ClearMap()
    {
        for(int i = 0; i < map.transform.childCount; i++)
        {
            Destroy(map.transform.GetChild(i).gameObject);
        }
    }
}
