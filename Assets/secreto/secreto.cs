using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secreto : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("Secreto", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("Secreto", false);
    }
}
