using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    public GameOverScreen GameOverScreen;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private int blasted = 5;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
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
            boxCollider.enabled = false;
            Deactivate();
            //blasted += 5;
            GameOverScreen.UpdateShotPoints(blasted);
        }
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
