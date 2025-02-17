using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerController : MonoBehaviour
{
    public AimController aim;

    public Animator shootAnim;
    public bool canShoot = true;

    public bool isDead = false;

    Rigidbody2D rbPlayer;
    public Animator animatorPlayer;

    public float speed = 3;
    public float jumpForce = 6.5f;
    bool isOnGround = true;
    public int vidaMax;
    public int vidaActual;
    float moveHorizontal;
    public int cura;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        vidaActual = vidaMax;
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        GiroApuntado();
        //Shooting();
    }

    void Movement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");


        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector2(1, 1);
            animatorPlayer.SetFloat("Speed", 1f);
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            animatorPlayer.SetFloat("Speed", 1f);
        }
        else if (moveHorizontal == 0)
        {
            animatorPlayer.SetFloat("Speed", 0f);
        }

        rbPlayer.velocity = new Vector2(moveHorizontal * speed, rbPlayer.velocity.y);
        //animatorPlayer.SetFloat("Move", Mathf.Abs(moveHorizontal));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Saltar();
        }
    }

    void GiroApuntado()
    {
        if (aim.apuntado < 90f && aim.apuntado > -90f)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-1, 1);
    }

    /*void Shooting()
    {
        if(isOnGround == true)
        {
            canShoot = true;
        }

        if (Input.GetMouseButtonDown(0) && canShoot && !isCooldown)
        {
            StartCoroutine(Cooldown());

            ImpulsoDisparo();

            shootAnim.SetTrigger("Shoot");
            canShoot = false;
        }
    }*/



    public void Saltar()
    {
        //salto
        if (isOnGround)
        {
            isOnGround = false;
            rbPlayer.velocity = new Vector2(moveHorizontal, jumpForce);
            animatorPlayer.SetBool("Jumping", true);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;

            animatorPlayer.SetBool("Jumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = false;

            animatorPlayer.SetBool("Jumping", true);
        }
    }
}
