using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 liveSpawn;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private Spawner spawner;
    private bool hit;


    private BoxCollider2D boxCollider;
    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        hit = false;
        anim.SetBool("hit", hit);
        boxCollider.enabled = true;
    }

    void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(-movementSpeed, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit) return;
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Deleter") || collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(DestroyEnemy());
        }
     }

    private IEnumerator DestroyEnemy()
    {
        hit = true;
        anim.SetBool("hit", hit);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
        //boxCollider.enabled = true;
        //this.gameObject.transform.position = spawnPoint;
        //yield return null;

        //Destroy(gameObject, delay);
    }

}
