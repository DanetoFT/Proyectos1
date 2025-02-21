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

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        vidaActual = vidaMax;
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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

        if (isOnGround)
        {
            transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        }

        // Guardamos la velocidad vertical (Y) para no modificarla al aplicar el impulso
        float currentYVelocity = rbPlayer.velocity.y;

        if (moveHorizontal > 0)
        {
            sprite.flipX = false;
            //transform.localScale = new Vector2(1, 1);
            animatorPlayer.SetFloat("Speed", 1f);
        }
        else if (moveHorizontal < 0)
        {
            sprite.flipX = true;
            //transform.localScale = new Vector2(-1, 1);
            animatorPlayer.SetFloat("Speed", 1f);
        }
        else if (moveHorizontal == 0)
        {
            sprite.flipX = false;
            animatorPlayer.SetFloat("Speed", 0f);
        }

        // Solo modificamos el valor de X, manteniendo el valor de Y intacto
        //rbPlayer.velocity = new Vector2(moveHorizontal * speed, currentYVelocity);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Saltar();
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
