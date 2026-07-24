using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseScreen;
    public GameObject loseScreen;
    public GameObject controlScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endScore;
    public int score;
    public GameObject[] platforms;
    public GameObject[] aliens;
    public GameObject[] spikes;
    public GameObject[] mobileButtons;
    public bool canSpawnPlatform;
    public bool alienSpawner;
    public bool spikeSpawner;
    public bool isGameover;
    public bool isPaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            isPaused = false;
            pauseScreen.SetActive(false);
            //if (!UnityEngine.Application.isMobilePlatform)
            {
                //for (int i = 0; i < mobileButtons.Length; i++)
                {
                    //mobileButtons[i].SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            scoreText.text = "Points: " + score.ToString();
            GameOver();
        }
    }

    public void GameOver()
    {
        if (isGameover)
        {
            Time.timeScale = 0f;
            scoreText.text = "";
            endScore.text = "Total: " + score.ToString();
            loseScreen.SetActive(true);
        }
        else
        {
            if (!isPaused)
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
            pauseScreen.SetActive(false);
        } else
        {
            Time.timeScale = 0f;
            isPaused = true;
            pauseScreen.SetActive(true);
        }
    }
    
    public void Resume()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
            pauseScreen.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Controls()
    {
        controlScreen.SetActive(true);
    }

    public void Return ()
    {
        controlScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Restart()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void SpawnPlatform()
    {

    }


}
