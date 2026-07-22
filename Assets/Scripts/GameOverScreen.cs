using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverScreen : MonoBehaviour
{

    public Text pointsText;
    public Text endPointsText;
    public GameObject ControlScreen;
    public int totalScore;
    

    public void UpdatePoints(int score)
    {
        totalScore += score;
        pointsText.text = "Points: " + totalScore.ToString();
    }

    public void UpdateShotPoints(int score)
    {
        totalScore += score;
        pointsText.text = "Points: " + totalScore.ToString();
    }

    public void PauseScreen()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        
    }

    public void GOSetup(int score)
    {
        pointsText.text = " ";
        gameObject.SetActive(true);
        endPointsText.text = totalScore + score + " POINTS";
    }

    public void ControlSetup()
    {
        gameObject.SetActive(true);
        ControlsButton();
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ControlsButton()
    {
        ControlScreen.SetActive(true);
    }

    public void ReturnButton()
    {
        ControlScreen.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
