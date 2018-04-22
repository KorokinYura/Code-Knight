using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int maxCommandsAmount;
    [Space]
    [SerializeField]
    private GameObject mainCommandsList;
    [SerializeField]
    private GameObject func1CommandsList;
    [SerializeField]
    private GameObject func2CommandsList;
    public GameObject CurCommandsList { get; private set; }
    [Space]
    [SerializeField]
    private GameObject commandsPool;
    public GameObject CommandsPool { get { return commandsPool; } }
    [SerializeField]
    private GameObject commandsDrag;
    public GameObject CommandsDrag { get { return commandsDrag; } }

    public static GameController Instance { get; private set; }

    private TickController tickController;

	private void Awake ()
    {
        Instance = this;
	}

    private void Start()
    {
        tickController = GetComponent<TickController>();
        CurCommandsList = mainCommandsList;
    }

    public void StartLevel()
    {
        tickController.StartTicks();
        // needs to be implemented
    }
    public void ResetLevel()
    {
        MapMakerController.Instance.LoadMap(false);
        tickController.StopTicks();
    }

    public void ShowCommandList(int index)
    {
        mainCommandsList.SetActive(false);
        func1CommandsList.SetActive(false);
        func2CommandsList.SetActive(false);

        switch (index)
        {
            case 0:
                mainCommandsList.SetActive(true);
                CurCommandsList = mainCommandsList;
                break;
            case 1:
                func1CommandsList.SetActive(true);
                CurCommandsList = func1CommandsList;
                break;
            case 2:
                func2CommandsList.SetActive(true);
                CurCommandsList = func2CommandsList;
                break;
            default:
                goto case 1;
        }
    }
    public IEnumerable<CommandUI> GetCommandUIs()
    {
        CommandUI[] arr = mainCommandsList.GetComponentsInChildren<CommandUI>();
        int amount = maxCommandsAmount;
        return GetRecCommandUIs(arr, ref amount);
    }

    private List<CommandUI> GetRecCommandUIs(CommandUI[] arr, ref int amount)
    {
        List<CommandUI> list = new List<CommandUI>();
        for (int i = 0; i < arr.Length; i++)
        {
            if (i == arr.Length) break;
            if (arr[i].Type == Command.Type.Func1)
            {
                list.AddRange(GetRecCommandUIs(func1CommandsList.GetComponentsInChildren<CommandUI>(), ref amount));
            }
            else if (arr[i].Type == Command.Type.Func2)
            {
                list.AddRange(GetRecCommandUIs(func2CommandsList.GetComponentsInChildren<CommandUI>(), ref amount));
            }
            else
            {
                if (amount == 0) break;
                list.Add(arr[i]);
                amount--;
            }
        }
        return list;
    }
}
