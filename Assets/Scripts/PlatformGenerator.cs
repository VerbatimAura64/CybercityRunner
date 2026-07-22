
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private GameObject platforms;
    [SerializeField] private GameObject[] spikes;
    [SerializeField] private GameObject[] pforms;
    public float obstacleSpawnTime = 2f;
    public float obstacleSpeed = 1f;
    private bool hit;
    private BoxCollider2D boxCollider;
    private Vector3 nextPlatform;
    [SerializeField] private float offset = 2.75f;
    private int count = 0;
    private float timeUntilDespawn;


    public int numOfSpike;

    private float timeUntilObstacleSpawn;



    // Start is called before the first frame update
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        SpawnLoop();

    
    }
    private void SpawnLoop() { 
        timeUntilObstacleSpawn += Time.deltaTime;



        if(timeUntilObstacleSpawn >= obstacleSpawnTime )
        {
            Spawn();
            //PlatformSpawn();
            timeUntilObstacleSpawn = 0f;
            PlatformSpawn();
            
        }
    }


   private void PlatformSpawn()
    {
        timeUntilDespawn += Time.deltaTime;

        // GameObject platformToSpawn = platforms;
        if (count < pforms.Length)
        {
            nextPlatform = new Vector3(platforms.transform.position.x + offset,
                                        platforms.transform.position.y,
                                        platforms.transform.position.z);

            pforms[count] = Instantiate(platforms, nextPlatform, Quaternion.identity);
            offset += 2.75f;
            count++;
  
        } else if (timeUntilDespawn > 10f)
        {
            Destroy(platforms);
        }
       

    }


    private void Spawn()
    {
        GameObject spikeToSpawn = spikes[Random.Range(0, spikes.Length)];

        if (spikeToSpawn.gameObject.tag != "Ground")
        {
            GameObject spawnedSpike = Instantiate(spikeToSpawn, transform.position, Quaternion.identity);

            Rigidbody2D spikeRB = spawnedSpike.GetComponent<Rigidbody2D>();
            spikeRB.linearVelocity = Vector2.left * obstacleSpeed;
        } else
        {
            GameObject spawnedSpike = Instantiate(spikeToSpawn, transform.position, Quaternion.identity);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Deleter")
        {
            hit = true;
            Debug.Log("Wall");
            DestroyGameObject();
        }

        /*else if (collision.gameObject.tag == "Deleter")
        {
            hit = true;
            
            DestroyGameObject();
        }*/
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
