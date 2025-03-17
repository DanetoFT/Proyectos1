using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapController : MonoBehaviour
{
    public PlayerController playerController;
    Rigidbody2D rb;
    Animator animator;

    bool damaged;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damaged = false;
    }

    public void Destruir()
    {
        Destroy(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("Destruir", .5f);


        if (collision.gameObject.tag == "Player" && !damaged)
        {
            playerController.vidaActual -= 3;
            Debug.Log(playerController.vidaActual);
            damaged = true;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;

        animator.SetTrigger("Cuerda");
    }
}
