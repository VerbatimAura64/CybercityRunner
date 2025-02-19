using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;


    private BoxCollider2D boxCollider;
    private Animator anim;
    private float blasted;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Deleter")
        {
            blasted += Time.deltaTime;
            hit = true;
            anim.SetBool("hit", hit);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            DestroyEnemy(.8f);
            //gameObject.SetActive(false);
            
            
            
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

    private void DestroyEnemy(float delay)
    {
        
        Destroy(gameObject, delay);
    }


}
