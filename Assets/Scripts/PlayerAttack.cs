using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject[] gunshots;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    public AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        //anim.SetTrigger("attack");
        cooldownTimer = 0;
        source.Play();
        //pool gunshots

        gunshots[FindGunshot()].transform.position = shotPoint.position;
        gunshots[FindGunshot()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindGunshot()
    {
        for(int i = 0; i < gunshots.Length; i++)
        {
            if (!gunshots[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
