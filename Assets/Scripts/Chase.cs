using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public MosquitoController[] controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(MosquitoController enemy in controller)
        {
            enemy.chase = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach(MosquitoController enemy in controller)
        {
            enemy.chase = false;
        }
    }
}
