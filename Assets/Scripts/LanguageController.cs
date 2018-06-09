﻿using UnityEngine;
using System.IO;

public static class LanguageController
{
    public static Lang LangType { get; set; }
    public static Language Language { get; private set; }

    public static event LangChangeDelegate ChangeLanguage;

    public static void LangLoad()
    {
        string jsonString;
#if UNITY_ANDROID && !UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath, "Languages/" + LangType.ToString().ToLower() + ".json");
        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        jsonString = reader.text.Trim();
#else
        jsonString = File.ReadAllText(Application.streamingAssetsPath + "/Languages/" + LangType.ToString().ToLower() + ".json");
#endif
        try
        {
            Language = JsonUtility.FromJson<Language>(jsonString);
        }
        catch (System.Exception ex)
        {
            GameObject.Find("InfoText").GetComponent<UnityEngine.UI.Text>().text = ex.ToString();
        }
        if(ChangeLanguage != null) ChangeLanguage();
    }

    public enum Lang
    {
        RUS, ENG, UKR
    }
    public delegate void LangChangeDelegate();
}

public class Language
{
    public string info;
    public string pauseName;
    public string winName;
    public string winText;
}