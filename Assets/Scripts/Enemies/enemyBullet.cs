using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class enemyBullet : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    Animator animator;

    public string[] damages;

    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            Destroyer();
        }
    }

    public void Destroyer()
    {
        AudioController.Instance.PlaySFX("BulletD");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerController = other.GetComponent<PlayerController>();

        rb.velocity = Vector2.zero;
        animator.SetTrigger("Destroy");
        Invoke("Destroyer", .2f);

        if (other.gameObject.tag == "Player")
        {
            playerController.vidaActual--;
            playerController.animatorPlayer.SetTrigger("Damage");
            playerController.UpdatedLifeBar(playerController.vidaActual);

            int random = Random.Range(0, 4);

            AudioController.Instance.PlaySFX(damages[random]);
        }
    }
}
