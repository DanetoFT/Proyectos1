using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MosquitoController : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public bool chase = false;
    public Transform startingPoint;

    Rigidbody2D rb;
    CapsuleCollider2D collider;

    public bool attacking;
    Vector2 suma;

    // Start is called before the first frame update
    void Start()
    {
        chase = false;
        suma = new Vector2(1, 1);
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(chase == true && !attacking)
        {
            Chase();
        }
        else if (chase == false && !attacking)
        {
            ReturnStart();
        }
        Flip();

        if (attacking == true)
        {
            Attack();
        }
    }

    void ReturnStart()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }

    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Attack()
    {
        chase = false;
        rb.bodyType = RigidbodyType2D.Static;
        collider.isTrigger = true;
        transform.position = new Vector2(player.transform.position.x +2, player.transform.position.y +2);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        attacking = true;
    }
}
