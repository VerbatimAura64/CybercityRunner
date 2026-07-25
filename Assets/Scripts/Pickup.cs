using UnityEngine;

public class Pickup : MonoBehaviour
{
    private BoxCollider2D thisCol;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisCol = GetComponent<BoxCollider2D>();   
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
