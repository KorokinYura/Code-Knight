using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(AchievementsSystem))]
public class Achievement : MonoBehaviour
{
    [SerializeField]
    private string achievementName;
    public string AchievementName { get { return achievementName; } }
    [SerializeField]
    [TextArea]
    private string achievementText;
    public string AchievementText { get { return achievementText; } }
    [SerializeField]
    private Sprite achievementSprite;
    public Sprite AchievementSprite { get { return achievementSprite; } }
    public bool IsActivated { get; private set; }

    public bool Unlock(AchievementsSystem sender)
    {
        if(GetComponent<AchievementsSystem>() == sender)
        {
            IsActivated = true;
            return true;
        }
        return false;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(new AchievementSave() { name = achievementName, isActivated = IsActivated});
    }
    public void FromJson(string str)
    {
        AchievementSave a = JsonUtility.FromJson<AchievementSave>(str);
        if(a.name == achievementName)
        {
            IsActivated = a.isActivated;
        }
    }
    [Serializable]
    private class AchievementSave
    {
        public string name;
        public bool isActivated;
    }
}
