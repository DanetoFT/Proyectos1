using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapController : MonoBehaviour
{
    public PlayerController playerController;
    Rigidbody2D rb;
    Animator animator;
    Animator playerAnimator;

    bool isActivated;

    bool damaged;

    public float tiempoCaida;

    public string[] damages;

    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damaged = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !damaged)
        {
            playerAnimator = collision.gameObject.GetComponent<Animator>();
            playerController.vidaActual -= 1;

            int random = Random.Range(0, 4);

            AudioController.Instance.PlaySFX(damages[random]);

            playerAnimator.SetTrigger("Damage");

            playerController.UpdatedLifeBar(playerController.vidaActual);
            Debug.Log(playerController.vidaActual);
            damaged = true;

            AudioController.Instance.PlaySFX("Rope");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivated)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;

            Invoke("CambioRb", tiempoCaida);

            animator.SetTrigger("Cuerda");

            isActivated = false;
        }

        rb.bodyType = RigidbodyType2D.Dynamic;

        Invoke("CambioRb", tiempoCaida);

        animator.SetTrigger("Cuerda");

        AudioController.Instance.PlaySFX("Rope");
    }

    public void CambioRb()
    {
        rb.bodyType = RigidbodyType2D.Static;
        damaged = true;
        AudioController.Instance.PlaySFX("Trampa");
    }
}
