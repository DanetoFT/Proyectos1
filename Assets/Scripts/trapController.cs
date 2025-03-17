using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapController : MonoBehaviour
{
    public PlayerController playerController;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Destruir()
    {
        Destroy(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("Destruir", .5f);

        if (collision.gameObject.tag == "Player")
        {
            playerController.vidaActual -= 3;
            Debug.Log(playerController.vidaActual);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
