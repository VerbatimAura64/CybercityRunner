using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //public ControlScreen ControlScreen;
    public GameOverScreen GameOverScreen;
    private int maxPlatform = 0;
    private int enemyBlasted = 0;

    public void GameOver()
    {
        GameOverScreen.GOSetup(maxPlatform + enemyBlasted);
    }

   public void Controls()
    {
        GameOverScreen.ControlSetup();
    }

}
