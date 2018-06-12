using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    [SerializeField]
    private string gameplaySceneName;
    [SerializeField]
    private string mapMakerSceneName;
    [SerializeField]
    private string mainMenuSceneName;
    [SerializeField]
    private string campaignsMenuSceneName;

    public static ScenesController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void ToGameplay()
    {
        StartCoroutine(LoadYourAsyncScene(gameplaySceneName));
        //SceneManager.LoadScene(gameplaySceneName);
    }

    public void ToMapMaker()
    {
        StartCoroutine(LoadYourAsyncScene(mapMakerSceneName));
        //SceneManager.LoadScene(mapMakerSceneName);
    }

    public void ToMainMenu()
    {
        StartCoroutine(LoadYourAsyncScene(mainMenuSceneName));
        //SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ToCampaignsMenu()
    {
        StartCoroutine(LoadYourAsyncScene(campaignsMenuSceneName));
        //SceneManager.LoadScene(campaignsMenuSceneName);
    }
    
    IEnumerator LoadYourAsyncScene(string name)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
