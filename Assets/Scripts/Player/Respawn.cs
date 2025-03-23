using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject player;
    public Transform respawn;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerController = collision.GetComponent<PlayerController>();
        player.transform.position = respawn.position;

        playerController.vidaActual--;
        playerController.animatorPlayer.SetTrigger("Damage");
        playerController.UpdatedLifeBar(playerController.vidaActual);
    }
}
