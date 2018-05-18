using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int maxCommandsAmount;
    public int MaxCommandsAmount { get { return maxCommandsAmount; } }
    public int CommandsAmount { get; private set; }
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
    [Space]
    [SerializeField]
    private bool trackCurCmndUI;
    [SerializeField]
    private Color curCmndUIColor;

    public static GameController Instance { get; private set; }

    private TickController tickController;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        tickController = GetComponent<TickController>();
        if (trackCurCmndUI) TickController.TicksEvent += CommandUIsTracker;
        CurCommandsList = mainCommandsList;
    }

    public void StartLevel()
    {
        tickController.StartTicks();
    }
    public void ResetLevel()
    {
        MapMakerController.Instance.LoadMap(MapMakerController.CurLoadString, false);
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
                goto case 0;
        }
    }

    private IEnumerator CommandUIsTracker(float tickTime)
    {
        Image image = null;
        Color color = Color.white;
        foreach (CommandUI c in GetCommandUIs() ?? new CommandUI[0])
        {
            yield return new WaitForSeconds(tickTime);
            
            if (c.transform.parent == mainCommandsList.transform) ShowCommandList(0);
            else if (c.transform.parent == func1CommandsList.transform) ShowCommandList(1);
            else if (c.transform.parent == func2CommandsList.transform) ShowCommandList(2);

            image = c.GetComponent<Image>();
            if (image != null)
            {
                color = image.color;
                image.color = curCmndUIColor;
                StartCoroutine(ReturnTrackedCmdColor(image, color, tickTime));
            }
        }
    }
    private IEnumerator ReturnTrackedCmdColor(Image image, Color color, float tickTime)
    {
        yield return new WaitForSeconds(tickTime);
        if (image != null) image.color = color;
    }

    public IEnumerable<CommandUI> GetCommandUIs()
    {
        if (mainCommandsList == null) return null;
        CommandUI[] arr = mainCommandsList.GetComponentsInChildren<CommandUI>();
        int amount = maxCommandsAmount;
        List<CommandUI> cmdsUIs = GetRecCommandUIs(arr, ref amount);
        CommandsAmount = maxCommandsAmount - amount;
        return cmdsUIs;
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
