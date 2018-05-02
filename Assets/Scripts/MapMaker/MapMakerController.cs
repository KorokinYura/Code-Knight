using System;
using System.Collections.Generic;
using UnityEngine;
using MapObjects;

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
    [Space]
    [SerializeField]
    private CommandsEditor mapObjectCommandsEditor;
    public CommandsEditor MapObjectCommandsEditor { get { return mapObjectCommandsEditor; } }

    public static MapMakerController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A)) SaveMap();
        if (Input.GetKeyDown(KeyCode.D)) LoadMap(true);
    }

    private string MapObjToSaveString(GameObject obj)
    {
        if (obj == null) return "";
        string str = obj.GetComponent<MapObjectController>().Type + ","
                + Mathf.Round(obj.transform.position.x) + ","
                + Mathf.Round(obj.transform.position.y) + ","
                + Mathf.Round(obj.transform.localEulerAngles.z / 90);

        Lever l = null;
        if (obj.GetComponent<MapObjectController>().Type == MapObjectType.Lever)
            l = obj.GetComponent<Lever>();
        if (l != null && l.TriggerObj != null)
            str += "," + MapObjToSaveString(l.TriggerObj);

        Enemy e = null;
        if (obj.GetComponent<MapObjectController>().Type == MapObjectType.Enemy)
            e = obj.GetComponent<Enemy>();
        if (e != null)
        {
            string c = "";
            for (int i = 0; i < e.Cmds.Length; i++)
            {
                c += e.Cmds[i].GetType().Name;
                if (i != e.Cmds.Length - 1)
                    c += ".";
            }
            str += ",{" + c + "}";
        }
        return str;
    }
    private GameObject SaveStringToMapObject(string str, bool forMapMaker)
    {
        try
        {
            string[] s = str.Split(',');
            MapObjectType type = (MapObjectType)Enum.Parse(typeof(MapObjectType), s[0]);

            GameObject obj;
            if (forMapMaker) obj = MapObjectUI.CreateMapObject(type);
            else obj = Instantiate(MapObjectsPrefabs[(int)type], map.transform);

            obj.transform.position = new Vector2(
                Int32.Parse(s[1]),
                Int32.Parse(s[2])
                );
            obj.transform.localEulerAngles = new Vector3(0, 0, Int32.Parse(s[3]) * 90);

            if (type == MapObjectType.Lever && s.Length > 4)
            {
                for (int i = 0; i < map.transform.childCount; i++)
                {
                    Transform t = map.transform.GetChild(i);
                    if (t.GetComponent<ITriggerable>() != null && (Vector2)t.position == new Vector2(Int32.Parse(s[5]), Int32.Parse(s[6])))
                    {
                        Destroy(t.gameObject);
                    }
                }

                int index = str.IndexOf(',');
                index = str.IndexOf(',', index + 1);
                index = str.IndexOf(',', index + 1);
                index = str.IndexOf(',', index + 1);

                obj.GetComponent<Lever>().TriggerObj = SaveStringToMapObject(str.Substring(index + 1), forMapMaker);
            }

            if (type == MapObjectType.Enemy && s[4].Length > 2)
            {
                string[] cStrs = s[4].Substring(1, s[4].Length - 2).Split('.');
                List<Command> cmds = new List<Command>();
                for (int i = 0; i < cStrs.Length; i++)
                {
                    if (cStrs[i] == Command.Type.GoForward.ToString()) cmds.Add(new GoForward(obj.gameObject));
                    else if (cStrs[i] == Command.Type.TurnLeft.ToString()) cmds.Add(new TurnLeft(obj.gameObject));
                    else if (cStrs[i] == Command.Type.TurnRight.ToString()) cmds.Add(new TurnRight(obj.gameObject));
                    else if (cStrs[i] == Command.Type.Use.ToString()) cmds.Add(new Use(obj.gameObject));
                }
                obj.GetComponent<Enemy>().SetCommands(cmds);
            }
            return obj;
        }
        catch
        {
            return null;
        }
    }
    public void SaveMap()
    {
        string saveStr = "";
        for (int i = 0; i < map.transform.childCount; i++)
        {
            GameObject obj = map.transform.GetChild(i).gameObject;
            saveStr += MapObjToSaveString(obj);
            if (i != map.transform.childCount - 1)
            {
                saveStr += "|";
            }
        }
        Clipboard = saveStr;
    }
    public void LoadMap(bool forMapMaker = false)
    {
        ClearMap();

        string[] strs = Clipboard.Split('|');
        for (int i = 0; i < strs.Length; i++)
        {
            SaveStringToMapObject(strs[i], forMapMaker);
        }
    }

    public void ClearMap()
    {
        for (int i = 0; i < map.transform.childCount; i++)
        {
            Destroy(map.transform.GetChild(i).gameObject);
        }
    }
}
