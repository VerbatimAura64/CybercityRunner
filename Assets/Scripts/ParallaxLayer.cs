using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Range(0f, 1f)] public float parallaxFactor;
    private Transform cam;
    private float startPos;
    private float camStartPos;
    private float spriteWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.transform;
        startPos = transform.position.x;
        camStartPos = cam.transform.position.x;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float cameraDelta = cam.position.x - camStartPos;
        float newPos = startPos + cameraDelta * parallaxFactor;
        transform.position = new Vector3(newPos, transform.position.y, transform.position.z);
        if (cam.position.x - transform.position.x >= spriteWidth)
        {
            startPos += spriteWidth;
        }
    }
}
