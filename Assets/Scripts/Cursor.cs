using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cursor : MonoBehaviour
{
    public Transform player;
    private float speed = .1f;
    public Dialogo dialogo;
    public GameObject cursor;
    public Animator volume;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogo.activate)
        {
            Chase();
        }
    }

    void Chase()
    {
        speed += Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        cursor.gameObject.SetActive(true);
        volume.SetTrigger("Volume");
    }

    void PlayerKiller()
    {
        AudioController.Instance.PlayMusic("musicaMenu");
        SceneManager.LoadScene("SampleScene");
    }

    void PlayClick()
    {
        AudioController.Instance.PlaySFX("Click");
        AudioController.Instance.musicSource.Stop();
        Invoke("PlayerKiller", .3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerController.playerDead = true;
        transform.position = player.transform.position;
        Invoke("PlayClick", 1f);
    }
}
