using UnityEngine;

public class Generator : MonoBehaviour
{
    //[SerializeField] private GameObject objGen;
    [SerializeField] private GameObject[] generatedObj;
    public float objSpawnTime = 3f;
    public float objSpeed = 0f;
    private bool hit;
    private BoxCollider2D boxCollider;
    private Vector3 nextPlatform;
    private float offset = 2.75f;
    private int count = 0;
    private float timeUntilSpawn;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnLoop();
    }

    private void SpawnLoop()
    {
        timeUntilSpawn += Time.deltaTime;

        if (objSpawnTime > timeUntilSpawn)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject objToSpawn = generatedObj[Random.Range(0, generatedObj.Length)];

        if (objToSpawn.tag == "Death" || objToSpawn.tag == "Enemy")
        {
            GameObject spawnedObj = Instantiate(objToSpawn, transform.position, Quaternion.identity);

            Rigidbody2D objRB = spawnedObj.GetComponent<Rigidbody2D>();
            objRB.linearVelocity = Vector2.left * objSpeed;
        } else
        {
            nextPlatform = new Vector3(objToSpawn.transform.position.x + offset, objToSpawn.transform.position.y, objToSpawn.transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            hit = true;
            DestroyGameObject();
        }
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

}
