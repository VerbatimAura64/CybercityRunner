using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GM gm;
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
   
    //public GameOverScreen GameOverScreen;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private int blasted = 5;

    private void Awake()
    {
        gm  = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
        {
            Deactivate();
        }
        if (collision.gameObject.tag == "Enemy" )//collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player"  && collision.gameObject.tag != "Safe" && collision.gameObject.tag != "Wall" && collision.gameObject.tag != 'Ground")
        {
            hit = true;
            //boxCollider.enabled = false;
            Deactivate();
            //blasted += 5;
            //GameOverScreen.UpdateShotPoints(blasted);
            gm.score += 5;
        }
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        
        hit = false;
        //boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        //Debug.Log(localScaleX + " " + direction);
        if (Mathf.Sign(localScaleX) != direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        gameObject.SetActive(true);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
