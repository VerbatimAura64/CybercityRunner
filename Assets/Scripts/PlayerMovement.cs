using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Serialize variables to edit them in editor
    public GM gm;
    [SerializeField]
    private float speed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject[] gunshots;
    [SerializeField] private float shotDistance;
    public HoldButton leftButton;
    public HoldButton rightButton;
    [SerializeField]private float horizontalInput;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private bool canAttack;
    private float cooldownTimer = Mathf.Infinity;
    public AudioSource source;
    //public GameOverScreen GameOverScreen;
    //public int score;
    //public Text pointsText;
 

    void Awake()
    { 
        //Gets this objects rigidbody to allow for movement
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (!UnityEngine.Application.isMobilePlatform)
        {
            InputHandle();
        }
        else
        {
            MobileInput();
        } 
        BulletRespawn();
    }

    void InputHandle()
    {
        Pause();
        if (!gm.isPaused)
        {
            Attack();
            Jump();
            Move();
            
        }
    }

    void MobileInput()
    {
        ButtonMove();
    }
    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            gm.PauseGame();
        }
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
            anim.SetTrigger("jump");
            grounded = false;
        } 
    }

    void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput != 0)
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
            
            if (horizontalInput > 0.01f)
                transform.localScale = Vector3.one;
            if (horizontalInput < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);
        }
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    void Attack()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && CanAttack())
        {
            cooldownTimer = 0;
            

            for(int i = 0; i <gunshots.Length; i++)
            {
                if(gunshots[i] != null && !gunshots[i].activeInHierarchy)
                {
                    gunshots[i].transform.position = shotPoint.position;
                    //gunshots[i].GetComponent<box>
                    gunshots[i].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
                    source.Play();//Debug.Log(Mathf.Sign(transform.localScale.x));
                    break;
                }
            }
        }
        cooldownTimer += Time.deltaTime;
    }

    private void BulletRespawn()
    {
        for (int i = 0; i < gunshots.Length; i++)
        {
            if (gunshots[i] != null && gunshots[i].activeInHierarchy)
            {
                if (gunshots[i].transform.position.x >= shotPoint.position.x + shotDistance)
                {
                    gunshots[i].SetActive(false);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collision with: {collision.gameObject.name}, tag: {collision.gameObject.tag}");
        Debug.Log(collision.GetContact(0).point);

        if (collision.gameObject.CompareTag("Safe"))
        {
            gm.score += 10;
        }
        if (collision.gameObject.CompareTag("Ground")) 
        { 
            grounded = true;
        } 
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other);
        if (other.gameObject.CompareTag("Death") || other.gameObject.CompareTag("Enemy"))
        {
            gm.isGameover = true;
            this.gameObject.SetActive(false);
        }
        Debug.Log($"Collision Trigger with: {other.gameObject.name}, tag: {other.gameObject.tag}");
       // Debug.Log(other.GetContacts(0).point);
    }
    public bool CanAttack()
    {
        return grounded;
    }

    public void JumpButton()
    {
        if (grounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
            anim.SetTrigger("jump");
            grounded = false;
        }
    }

    public void ShootButton()
    {
        if (cooldownTimer > attackCooldown && CanAttack())
        {
            cooldownTimer = 0;


            for (int i = 0; i < gunshots.Length; i++)
            {
                if (gunshots[i] != null && !gunshots[i].activeInHierarchy)
                {
                    gunshots[i].transform.position = shotPoint.position;
                    //gunshots[i].GetComponent<box>
                    gunshots[i].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
                    source.Play();//Debug.Log(Mathf.Sign(transform.localScale.x));
                    break;
                }
            }
        }
        cooldownTimer += Time.deltaTime;
    }

    public void ButtonMove()
    {
        if (leftButton.IsHeld || rightButton.IsHeld)
        {
            if (leftButton.IsHeld) MoveLeft();
            if (rightButton.IsHeld) MoveRight();
        } else
        {
            horizontalInput = 0f;
        }
    
    }


    public void MoveLeft()
    {
        //horizontalInput = leftButton.direction;
        //float horizontal = 0;
        //if (leftButton.IsHeld)
        if (leftButton.IsHeld)
        {
            if (horizontalInput >= -1)
            {
                horizontalInput += -.1f;
            }

        } else
        {
            horizontalInput = 0f;
        }
        if (horizontalInput != 0)
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
            if (horizontalInput < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);

            
        }
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    public void MoveRight()
    {
        //horizontalInput = rightButton.direction;
        //float horizontalRight = 0;
        if (rightButton.IsHeld)
        {
            if(horizontalInput <= 1)
            {
                horizontalInput += .1f;
            }
            
        }
        else
        {
            horizontalInput = 0f;
        }
        //Debug.Log("Right:" + rightButton.IsHeld + " " + horizontalInput);
        if (horizontalInput != 0)
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if (horizontalInput > 0.01f)
                transform.localScale = Vector3.one;



            
        }
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }
}
