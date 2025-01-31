using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    }

    void Movement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");


        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }

        if (moveHorizontal < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        rbPlayer.velocity = new Vector2(moveHorizontal * speed, rbPlayer.velocity.y);
        animatorPlayer.SetFloat("Move", Mathf.Abs(moveHorizontal));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Saltar();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Atacar();
        }
    }

    public void Atacar()
    {
        //Ataque
        if (isOnGround)
        {
            animatorPlayer.SetTrigger("Attack");
        }

    }

    public void Saltar()
    {
        //salto
        if (isOnGround)
        {
            isOnGround = false;
            rbPlayer.velocity = new Vector2(moveHorizontal, jumpForce);
            animatorPlayer.SetTrigger("Jump");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (vidaActual < vidaMax)
        {
            vidaActual += cura;
            Debug.Log("Vida actual: " + vidaActual);
        }
    }
}
