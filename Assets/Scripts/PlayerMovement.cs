using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Serialize variables to edit them in editor
    [SerializeField]
    private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    public GameOverScreen GameOverScreen;
    public int score;
    public Text pointsText;
 

    void Awake()
    { 
        //Gets this objects rigidbody to allow for movement
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        //
        float horizontalInput = Input.GetAxis("Horizontal");
        //This is how we move left and right
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        //This will flip the sprite based on the input direction we get
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //Allows Jumping
        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        if (Input.GetKey(KeyCode.Tab))
        {
            GameOverScreen.PauseScreen();
        }


        //Set animator parameter
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
        //Debug.Log(grounded);
    }

    private void Jump()
    {
        if (grounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
            anim.SetTrigger("jump");
            grounded = false;
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Safe")
        {
            score += 10;
        }
        if (collision.gameObject.tag == "Ground") 
        { 
            grounded = true;
            //score += 10;
        } 
        
    }
    public bool canAttack()
    {
        return grounded;
    }
}
