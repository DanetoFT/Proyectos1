using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxActivator : MonoBehaviour
{
    public bool playerNear;

    private void Start()
    {
        playerNear = false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        playerNear = true;
    }
}
