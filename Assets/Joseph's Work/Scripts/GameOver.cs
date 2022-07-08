using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public string MainMenu;
    public string RestartGame;
    public GameObject loadingscreen;
    public GameObject gameoverscreen;
    private int counter;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        TimeManager.Instance.stopStopWatch();

        loadingscreen.SetActive(false);
        gameoverscreen.SetActive(true);
        
        scoreText.text = TimeManager.Instance.timeToDisplay.ToString(@"mm\:ss\:fff");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = TimeManager.Instance.timeToDisplay.ToString(@"mm\:ss\:fff");
    }

    public void restartGame()
    {
        counter = 1;
        loadingscreen.SetActive(true);
        TimeManager.Instance.measuredTime = 0;
       
        StartCoroutine(LoadLevelAsync());

    }

    public void MainMenuGame()
    {
        counter = 0;
        loadingscreen.SetActive(true);
        TimeManager.Instance.measuredTime = 0;
        StartCoroutine(LoadLevelAsync());
    }

    private IEnumerator LoadLevelAsync()
    {
        AsyncOperation asyncLoad;
        if (counter == 1)
        {
            asyncLoad = SceneManager.LoadSceneAsync(RestartGame);
        }
        else
        {
            asyncLoad = SceneManager.LoadSceneAsync(MainMenu);
        }
       

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
  
}

