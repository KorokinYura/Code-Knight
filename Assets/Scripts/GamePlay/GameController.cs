using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject commandsList;
    public GameObject CommandsList { get { return commandsList; } }
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
    }

    public void StartLevel()
    {
        tickController.StartTicks();
        // needs to be implemented
    }
    public void ResetLevel()
    {

    }
}
