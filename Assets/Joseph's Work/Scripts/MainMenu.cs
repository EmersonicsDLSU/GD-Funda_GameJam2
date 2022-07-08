using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject optionscreen;
    public GameObject gamescreen;
    public GameObject loadingscreen;
    public GameObject howToPlayScreen;

    // Start is called before the first frame update
    void Start()
    {
        gamescreen.SetActive(true);
        optionscreen.SetActive(false);
        loadingscreen.SetActive(false);
        howToPlayScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        optionscreen.SetActive(false);
        gamescreen.SetActive(true);
        loadingscreen.SetActive(true);
        StartCoroutine(LoadLevelAsync());
        
    }
    private IEnumerator LoadLevelAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    public void OpenOptions()
    {
        optionscreen.SetActive(true);
        gamescreen.SetActive(false);
        loadingscreen.SetActive(false);

    }
    public void CloseOptions()
    {
        optionscreen.SetActive(false);
        gamescreen.SetActive(true);
        loadingscreen.SetActive(false);

    }

    public void OpenHTP()
    {
        howToPlayScreen.SetActive(true);
        gamescreen.SetActive(false);
        loadingscreen.SetActive(false);

    }

    public void CloseHTP()
    {
        howToPlayScreen.SetActive(false);
        gamescreen.SetActive(true);
        loadingscreen.SetActive(false);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
