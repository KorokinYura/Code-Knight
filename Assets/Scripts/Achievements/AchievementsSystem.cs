using System;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsSystem : MonoBehaviour
{
    private const string achievementsSaveKey = "Achievements";

    [SerializeField]
    private Text achievementUnlockText;
    [SerializeField]
    private Image achievementUnlockSprite;
    [Space]
    [SerializeField]
    private GameObject achievementsScrollView;
    [SerializeField]
    private GameObject achievementPrefab;

    private static AchievementsSystem instance;

    private Achievement[] achievements;
    private bool isActivated = false;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        Activate();
        LoadAchievements();
    }

    public static void Activate()
    {
        instance.achievements = instance.GetComponents<Achievement>();
        if (instance.achievements.Length > 0) instance.isActivated = true;
    }

    public static bool UnclockAchievement(string achievementName)
    {
        if (!instance.isActivated) return false;

        for (int i = 0; i < instance.achievements.Length; i++)
        {
            if (instance.achievements[i].AchievementName == achievementName)
            {
                if (instance.achievements[i].IsActivated) return false;

                instance.achievements[i].Unlock(instance);
                instance.ShowUnlockAchievement(instance.achievements[i]);

                instance.SaveAchievements();
                return true;
            }
        }

        return false;
    }

    private void SaveAchievements()
    {
        AchievementsSave achSav = new AchievementsSave() { achievements = new string[achievements.Length] };
        for (int i = 0; i < achievements.Length; i++)
        {
            achSav.achievements[i] = achievements[i].ToJson();
        }
        PlayerPrefs.SetString(achievementsSaveKey, JsonUtility.ToJson(achSav));
    }

    private void LoadAchievements()
    {
        AchievementsSave achSav = JsonUtility.FromJson<AchievementsSave>(PlayerPrefs.GetString(achievementsSaveKey));
        if (achSav == null) return;

        foreach (Achievement a in achievements)
        {
            for(int i = 0; i < achSav.achievements.Length; i++)
            {
                a.FromJson(achSav.achievements[i]);
            }
        }
    }

    private void ShowUnlockAchievement(Achievement achievement)
    {
        Animator anim = GetComponent<Animator>();
        if (anim == null) return;
        achievementUnlockText.text = achievement.AchievementName;
        achievementUnlockSprite.sprite = achievement.AchievementSprite;
        anim.SetTrigger("Unlock");
    }

    public void ShowAchievementsList()
    {
        GameObject curAch;
        achievementsScrollView.SetActive(true);
        achievementsScrollView.transform.GetChild(0).GetChild(0).position -= new Vector3(0, 200, 0);
        foreach(Achievement ach in achievements)
        {
            curAch = Instantiate(achievementPrefab, achievementsScrollView.transform.GetChild(0).GetChild(0));
            curAch.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = ach.AchievementSprite;
            curAch.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = ach.IsActivated ? "UNLOCKED" : "LOCKED";
            curAch.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ach.AchievementName;
            curAch.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = ach.AchievementText;
        }
    }

    public void HideAchievementsList()
    {
        Transform t = achievementsScrollView.transform.GetChild(0).GetChild(0);
        for(int i = 0; i < t.childCount; i++)
        {
            Destroy(t.GetChild(i).gameObject);
        }
        achievementsScrollView.SetActive(false);
    }

    [Serializable]
    private class AchievementsSave
    {
        public string[] achievements;
    }
}
