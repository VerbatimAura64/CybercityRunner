using UnityEngine;

public class Deleter : MonoBehaviour
{
    //private BoxCollider2D boxCollider;

    private void Awake()
    {
        //boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Destroyed!!");
            Debug.Log(collision.gameObject.name);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Destroyed!!");
            Debug.Log(other.gameObject.name);
            Destroy(other.gameObject);
        }
    }
}
