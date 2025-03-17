using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerController : MonoBehaviour
{
    [Header("Apuntado y Disparo")]
    public AimController aim;
    public Animator shootAnim;
    public bool canShoot = true;

    public bool isDead = false;

    [Header("PlayerStats")]
    Rigidbody2D rbPlayer;
    public Animator animatorPlayer;
    public float speed = 3;
    public float jumpForce = 6.5f;
    public bool isOnGround = true;
    public int vidaMax;
    public int vidaActual;
    float moveHorizontal;
    public int cura;
    public float fallMultiplier;
    Vector2 vecGravity;
    public float move;

    SpriteRenderer sprite;

    public GameObject[] vidas;

    // Start is called before the first frame update
    void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        isDead = false;
        vidaActual = vidaMax;
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!aim.impulse)
        {
            Movement();
        }
        GiroApuntado();
        //Shooting();
    }

    void Movement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        move = moveHorizontal;

        //transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);

        rbPlayer.velocity = new Vector2(speed * moveHorizontal, rbPlayer.velocity.y);

        float currentYVelocity = rbPlayer.velocity.y;

        if (moveHorizontal > 0)
        {
            //sprite.flipX = false;
            transform.localScale = new Vector2(1, 1);
            animatorPlayer.SetFloat("Speed", 1f);
        }
        else if (moveHorizontal < 0)
        {
            //sprite.flipX = true;
            transform.localScale = new Vector2(-1, 1);
            animatorPlayer.SetFloat("Speed", 1f);
        }
        else if (moveHorizontal == 0)
        {
            //sprite.flipX = false;
            animatorPlayer.SetFloat("Speed", 0f);
        }

        //rbPlayer.velocity = new Vector2(moveHorizontal * speed, currentYVelocity);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Saltar();
        }

        if (rbPlayer.velocity.y < 0)
        {
            rbPlayer.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }
    }

    void GiroApuntado()
    {
        if (aim.apuntado < 90f && aim.apuntado > -90f)
        {
            //sprite.flipX = false;
            transform.localScale = new Vector2(1, 1);
        }

        /*if (aim.apuntado > 90f && aim.apuntado < -90f)
        {
            sprite.flipX = true;
            //transform.localScale = new Vector2(1, 1);
        }*/
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //sprite.flipX = true;
        transform.localScale = new Vector2(-1, 1);
    }

    public void Saltar()
    {
        //salto
        if (isOnGround)
        {
            isOnGround = false;
            //rbPlayer.velocity += new Vector2(rbPlayer.velocity.x, jumpForce);
            rbPlayer.AddForce(new Vector2(rbPlayer.velocity.x, jumpForce), ForceMode2D.Impulse);
            animatorPlayer.SetBool("Jumping", true);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Trap")
        {
            isOnGround = true;

            aim.impulse = false;

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
