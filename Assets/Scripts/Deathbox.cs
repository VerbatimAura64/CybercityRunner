using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deathbox : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    //public Text text;
    private bool death; 
    public GameOverScreen GameOverScreen;
    private int spikesAvoided = 10;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        //txt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (death) return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Death" || collision.gameObject.tag == "Enemy")
        {
            death = true;
            Deactivate();
            GameOver();
        } else if (collision.gameObject.tag == "Safe")
        {
            GameOverScreen.UpdatePoints(spikesAvoided);
        }


    }

    private void GameOver()
    {
        GameOverScreen.GOSetup(0);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
        //text.IsActive() == true;
    }
}
