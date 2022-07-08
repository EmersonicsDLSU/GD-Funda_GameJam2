using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static string UserGameScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }

    public static void EndGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        UserGameScore = TimeManager.Instance.timeToDisplay.ToString(@"mm\:ss\:fff");
        TimeManager.Instance.stopStopWatch();
        SceneManager.LoadScene("Joseph's Work/Scenes/GAME OVER");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
