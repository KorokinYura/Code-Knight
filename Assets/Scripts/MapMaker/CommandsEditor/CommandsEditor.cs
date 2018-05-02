using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandsEditor : MonoBehaviour
{
    [SerializeField]
    private GameObject commandsPool;
    public GameObject CommandsPool { get { return commandsPool; } }
    [SerializeField]
    private GameObject commandsList;
    public GameObject CommandsList { get { return commandsList; } }

    private Enemy commandsEditorAim;
    public Enemy CommandsEditorAim
    {
        get
        {
            return commandsEditorAim;
        }
        set
        {
            gameObject.SetActive(true);
            commandsEditorAim = value;
        }
    }

    public static CommandsEditor Instance;

    private void Start()
    {
        Instance = this;
    }

    public void SetCommandsToCurAim()
    {
        List<Command> cmds = new List<Command>();
        for(int i = 0; i < commandsList.transform.childCount; i++)
        {
            CommandsEditorCommand c = commandsList.transform.GetChild(i).GetComponent<CommandsEditorCommand>();
            if (c != null)
            {
                if (c.Type == CommandsEditorCommand.CmdType.GoForward) cmds.Add(new GoForward(CommandsEditorAim.gameObject));
                else if (c.Type == CommandsEditorCommand.CmdType.Left) cmds.Add(new TurnLeft(CommandsEditorAim.gameObject));
                else if (c.Type == CommandsEditorCommand.CmdType.Right) cmds.Add(new TurnRight(CommandsEditorAim.gameObject));
                else if (c.Type == CommandsEditorCommand.CmdType.Use) cmds.Add(new Use(CommandsEditorAim.gameObject));
            }
        }
        commandsEditorAim.SetCommands(cmds);
    }
}
