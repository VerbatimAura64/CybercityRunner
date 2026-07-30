using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    public Vector3 spawnPoint;
    private Spawner spawner;
    public GM gm;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        
    }

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
        spawner = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Spawner>();
        //liveSpawn = spawner.spikeSpawnpoint.transform.position;
        //spawnPoint = new Vector3(spawner.spikeLiveSpawn.x, 
                             //   spawner.spikeOrigin.y, 
                           //     spawner.spikeLiveSpawn.z);
        //this.gameObject.transform.position = spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        //spawnPoint = gm.spawner1;//new Vector3(spawner.spikeLiveSpawn.x,
        //                        spawner.spikeOrigin.y,
        //                        spawner.spikeLiveSpawn.z);
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(-movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Deleter"))
        {
            Deactivate();
            //Destroy(gameObject);
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
        //this.gameObject.transform.position = spawnPoint;
    }
}
