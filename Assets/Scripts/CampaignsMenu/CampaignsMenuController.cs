﻿using System.Text;
using UnityEngine;

public class CampaignsMenuController : MonoBehaviour
{
    private const string campaign1SaveName = "C1Save";

    [SerializeField]
    private Level[] levels;

    private static int curLevelIndex = -1;
    private static int curLevelScore;

    public static CampaignsMenuController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        MapMakerController.CurLoadString = null;
    }

    private void Start()
    {
        if (curLevelIndex > -1 && curLevelScore > 0)
        {
            LoadProgress();
            int score = levels[curLevelIndex].HighScore;
            if (score == 0 || score > curLevelScore)
                levels[curLevelIndex].HighScore = curLevelScore;
            curLevelScore = 0;
            curLevelIndex = -1;
            SaveProgress();
        }
        LoadProgress();
    }

    private void LoadProgress()
    {
        if (!PlayerPrefs.HasKey(campaign1SaveName))
        {
            PlayerPrefs.SetString(campaign1SaveName, CreateEmptySaveString());
        }
        SaveStringToLevels(PlayerPrefs.GetString(campaign1SaveName));
    }
    private void SaveStringToLevels(string saveString) // SaveString : {pointsAmount0;pointsAmount1;...;pointsAmountN}
    {
        bool availables = true;
        string[] levelsSaves = saveString.Split(';');
        for(int i = 0; i < levelsSaves.Length && i < levels.Length; i++)
        {
            int highScore;
            int.TryParse(levelsSaves[i], out highScore);
            levels[i].HighScore = availables ? (highScore > 0 ? highScore : 0) : 0;
            levels[i].Available = availables;
            availables = availables ? highScore > 0 : false;
        }
    }

    private void SaveProgress()
    {
        if (!PlayerPrefs.HasKey(campaign1SaveName))
        {
            PlayerPrefs.SetString(campaign1SaveName, CreateEmptySaveString());
        }
        PlayerPrefs.SetString(campaign1SaveName, LevelsToSaveString());
    }
    private string LevelsToSaveString()
    {
        StringBuilder sb = new StringBuilder();
        foreach(Level level in levels)
        {
            sb.Append(string.Format("{0};", (level.HighScore > 0 ? level.HighScore.ToString() : string.Empty)));
        }
        sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }

    private string CreateEmptySaveString()
    {
        StringBuilder sb = new StringBuilder();
        string separator = ";";
        for(int i = 0; i < levels.Length - 1; i++)
        {
            sb.Append(separator);
        }
        return sb.ToString();
    }

    public void SetCurrentLevel(Level level)
    {
        for(int i = 0; i < levels.Length; i++)
        {
            if(level == levels[i])
            {
                curLevelIndex = i;
                return;
            }
        }
    }
    public static void SetCurrentLevelScore(int score)
    {
        curLevelScore = score > 0 ? score : 0;
    }
}
