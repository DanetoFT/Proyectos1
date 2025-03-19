using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoSangre : MonoBehaviour
{
    public GameObject player;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
    }

    void Flip()
    {
        if (transform.position.x > player.transform.position.x)
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
        anim.SetTrigger("Death");

        Invoke("Destruir", .3f);
    }
}
