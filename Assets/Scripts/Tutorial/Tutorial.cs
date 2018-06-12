using System;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private TutorialInfo[] tutorials;
    private int curTutorial;
    [Space]
    [SerializeField]
    private GameObject tutorialPnl;
    [SerializeField]
    private Image tutorialImage;
    [SerializeField]
    private Text tutorialName;
    [SerializeField]
    private Text tutorialText;

    public static Tutorial Instance { get; private set; }

    private bool only = false;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowTutorial()
    {
        ShowTutorial(0, false);
    }
    public void ShowTutorial(int index = 0, bool only = false)
    {
        this.only = only;
        curTutorial = index;
        AchievementsSystem.UnclockAchievement("Tutorial");
        if (tutorials.Length > curTutorial)
        {
            tutorialPnl.SetActive(true);
            tutorialImage.sprite = tutorials[curTutorial].sprite;
            switch (tutorials[curTutorial].index)
            {
                case 0:
                    tutorialName.text = LanguageController.Language.t1Name;
                    tutorialText.text = LanguageController.Language.t1Text;
                    break;
                case 1:
                    tutorialName.text = LanguageController.Language.t2Name;
                    tutorialText.text = LanguageController.Language.t2Text;
                    break;
                case 2:
                    tutorialName.text = LanguageController.Language.t3Name;
                    tutorialText.text = LanguageController.Language.t3Text;
                    break;
                case 3:
                    tutorialName.text = LanguageController.Language.t4Name;
                    tutorialText.text = LanguageController.Language.t4Text;
                    break;
                case 4:
                    tutorialName.text = LanguageController.Language.t5Name;
                    tutorialText.text = LanguageController.Language.t5Text;
                    break;
                case 5:
                    tutorialName.text = LanguageController.Language.t6Name;
                    tutorialText.text = LanguageController.Language.t6Text;
                    break;
            }
        }
    }

    public void ShowNextTutorial()
    {
        if (only || ++curTutorial >= tutorials.Length)
        {
            HideTutorial();
            return;
        }
        ShowTutorial(curTutorial);
    }

    public void HideTutorial()
    {
        only = false;
        curTutorial = 0;
        tutorialPnl.SetActive(false);
    }
    
    [Serializable]
    private class TutorialInfo
    {
        public int index;
        public Sprite sprite;
    }
}
