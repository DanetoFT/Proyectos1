using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    Animator animator;
    SpriteRenderer sprite;

    float timer;
    private GameObject player;

    public float range;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance < range)
        {
            timer += Time.deltaTime;
            animator.SetBool("Attack", true);

            Vector3 direction = player.transform.position - transform.position;
            float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot);

            if(rot > 90f && rot > 90f)
            {
                //sprite.flipY = true;
                transform.localScale = new Vector2(1, -1);
            }
            
            if (rot < 90f && rot > -90f)
            {
                //sprite.flipY = false;
                transform.localScale = new Vector2(1, 1);
            }

            if (timer > 1.8f)
            {
                timer = 0;
                Shoot();
            }
        }
        else
        {
            animator.SetBool("Attack", false);
        }

    }

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    void Destruir()
    {
        Destroy(gameObject);
    }
    void Death()
    {
        CancelInvoke();

        animator.SetTrigger("Death");

        AudioController.Instance.PlaySFX("Splat");

        Invoke("Destruir", .3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Death();
        }
    }
}
