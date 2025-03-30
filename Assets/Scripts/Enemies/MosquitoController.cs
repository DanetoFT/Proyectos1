using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MosquitoController : MonoBehaviour
{
    public PlayerController playerController;

    public float speed;
    public GameObject player;
    public bool chase = false;
    public Transform startingPoint;

    Rigidbody2D rb;
    CircleCollider2D cCollider;
    Animator anim;

    Animator playerAnim;

    public bool attacking;
    Vector2 suma;

    public bool isDead;

    public string[] damages;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        chase = false;
        suma = new Vector2(1, 1);
        rb = GetComponent<Rigidbody2D>();
        cCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(chase == true && !attacking && !isDead)
        {
            Chase();
        }
        else if (chase == false && !attacking && !isDead)
        {
            ReturnStart();
        }
        Flip();

        if (attacking == true && !isDead)
        {
            Attack();
        }

        if(playerController.move < 0 || playerController.move > 0)
        {
            anim.SetFloat("Walk" ,playerController.move);
        }
        else
        {
            anim.SetFloat("Walk", 0);
        }
    }

    void ReturnStart()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
        anim.SetBool("Angry", false);
    }

    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        anim.SetBool("Angry", true);
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        chase = false;
        rb.bodyType = RigidbodyType2D.Static;
        //cCollider.isTrigger = true;
        transform.position = new Vector2(player.transform.position.x +1.5f, player.transform.position.y +1.3f);
    }

    public void Damage()
    {
        int random = Random.Range(0, 4);

        AudioController.Instance.PlaySFX(damages[random]);

        if(playerController.vidaActual > 0)
        {
            playerController.vidaActual--;
            Debug.Log(playerController.vidaActual);
            playerAnim.SetTrigger("Damage");

            playerController.UpdatedLifeBar(playerController.vidaActual);
        }
        else if (playerController.vidaActual <= 0)
        {
            playerController.vidaActual = 0;
            playerController.UpdatedLifeBar(0);
        }
    }

    public IEnumerator Damager()
    {
        Damage();

        yield return new WaitForSeconds(1f);
    }

    void Flip()
    {
        if(transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void Destruir()
    {
        Destroy(gameObject);
    }
    void Death()
    {
        //rb.bodyType = RigidbodyType2D.Dynamic;
        //collider.isTrigger = false;
        chase = false;
        attacking = false;

        CancelInvoke();

        AudioController.Instance.PlaySFX("Splat");

        anim.SetTrigger("Death");

        Invoke("Destruir", .3f);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attacking = true;
            playerAnim = collision.gameObject.GetComponent<Animator>();
            //StartCoroutine(Damager());
            InvokeRepeating("Damage", .2f, .9f);
        }
        else if(collision.gameObject.tag == "Bullet")
        {
            isDead = true;

            Vector3 bulletPos = collision.transform.position;
            bulletPos.z = 0;
            Vector3 bulletRotation = bulletPos - transform.position;

            Vector2 knockbackDirection = (transform.position - bulletPos).normalized;

            rb.velocity = knockbackDirection * 15;
            
            Death();
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            isDead = true;
            Death();
        }
        else if (collision.gameObject.tag == "Player" && !attacking)
        {
            attacking = true;
            playerAnim = collision.gameObject.GetComponent<Animator>();
            InvokeRepeating("Damage", .2f, .9f);
        }
    }
}
