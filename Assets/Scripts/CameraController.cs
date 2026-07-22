using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public float xOffset = 1f;
    public Transform target;




    /*[SerializeField] private float speed;
    private float currentPosX;
    private Vector3 Velocity = Vector3.zero;*/

 

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
    
        if (horizontalInput > 0.01f)
        {
            Vector3 newPos = new Vector3(target.position.x + xOffset, target.position.y + yOffset, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }
        if (horizontalInput < -0.01f)
        {
            Vector3 newPos = new Vector3(target.position.x - xOffset, target.position.y + yOffset, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }

        

        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.positionz),
            //ref Velocity, speed * Time.deltaTime);
    }
}
