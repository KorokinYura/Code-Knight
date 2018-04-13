using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMakerController : MonoBehaviour
{
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
}
