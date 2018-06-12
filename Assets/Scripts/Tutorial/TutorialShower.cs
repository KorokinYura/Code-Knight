using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialShower : MonoBehaviour
{
    private static string tutorialsShownKey = "TutorialsShown"; // "0,0,0,0,0,0"

    [SerializeField]
    private int indexToShow;

	void Start ()
    {
        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            switch (CampaignsMenuController.CurLevelIndex)
            {
                case 0:
                    indexToShow = 1;
                    break;
                case 1:
                    indexToShow = 2;
                    break;
                case 2:
                    indexToShow = 3;
                    break;
                case 4:
                    indexToShow = 4;
                    break;
                case 5:
                    indexToShow = 5;
                    break;
            }
            
        }
        if (!PlayerPrefs.HasKey(tutorialsShownKey))
        {
            PlayerPrefs.SetString(tutorialsShownKey, "0,0,0,0,0,0");
        }

        string[] arr = PlayerPrefs.GetString(tutorialsShownKey).Split(',');
        if(arr[indexToShow] == "0")
        {
            Tutorial.Instance.ShowTutorial(indexToShow, true);
            arr[indexToShow] = "1";
        }
        PlayerPrefs.SetString(tutorialsShownKey, string.Format("{0},{1},{2},{3},{4},{5}", arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]));
	}
}
