using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void InfoBtnClick()
    {
        AchievementsSystem.UnclockAchievement("Creators");
    }
}
