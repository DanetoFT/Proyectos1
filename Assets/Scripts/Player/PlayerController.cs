using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Apuntado y Disparo")]
    public AimController aim;
    public Animator shootAnim;
    public bool canShoot = true;

    public bool isDead = false;

    [Header("PlayerStats")]
    Rigidbody2D rbPlayer;
    public Animator animatorPlayer;
    public float speed = 3;
    public float jumpForce = 6.5f;
    public bool isOnGround = true;
    public int vidaMax;
    public int vidaActual;
    float moveHorizontal;
    public int cura;
    public float fallMultiplier;
    Vector2 vecGravity;
    public float move;
    float coyoteTime = 0.1f;
    float coyoteTimeCounter;

    public bool playerDead;

    public bool isWalking;

    public Transform playerTransform;

    SpriteRenderer sprite;

    public GameObject[] vidas;

    public static bool isPaused;

    public GameObject menu;
    public GameObject muerte;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        isDead = false;
        vidaActual = vidaMax;
        rbPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerDead = false;

        AudioController.Instance.PlayMusic("Juego1");
    }

    // Update is called once per frame
    void Update()
    {
        if (!aim.impulse && !playerDead && !isPaused)
        {
            Movement();
        }
        GiroApuntado();
        //Shooting();

        if (playerDead)
        {
            rbPlayer.velocity = Vector2.zero;
        }

        if (vidaActual <= 0)
        {
            playerDead = true;

            muerte.gameObject.SetActive(true);
            //isPaused = true;
            //Time.timeScale = 0f;
            AudioController.Instance.musicSource.Stop();
            AudioController.Instance.sfxSource.Stop();
        }

        if (isOnGround)
        {
            isWalking = true;

            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            menu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
            AudioController.Instance.musicSource.Pause();
            AudioController.Instance.sfxSource.Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            menu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
            AudioController.Instance.musicSource.UnPause();
            AudioController.Instance.sfxSource.UnPause();
        }
    }

    public void CambioEscena()
    {
        playerDead = false;
        //Time.timeScale = 1f;
        isPaused = false;
        AudioController.Instance.musicSource.Stop();
        SceneManager.LoadScene("Game");
    }

    public void UnPause()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        AudioController.Instance.musicSource.UnPause();
        AudioController.Instance.sfxSource.UnPause();
    }

    void Movement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        move = moveHorizontal;

        //transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);

        rbPlayer.velocity = new Vector2(speed * moveHorizontal, rbPlayer.velocity.y);

        float currentYVelocity = rbPlayer.velocity.y;

        if (moveHorizontal > 0)
        {
            //sprite.flipX = false;
            transform.localScale = new Vector2(1, 1);
            animatorPlayer.SetFloat("Speed", 1f);
        }
        else if (moveHorizontal < 0)
        {
            //sprite.flipX = true;
            transform.localScale = new Vector2(-1, 1);
            animatorPlayer.SetFloat("Speed", 1f);
        }
        else if (moveHorizontal == 0)
        {
            //sprite.flipX = false;
            animatorPlayer.SetFloat("Speed", 0f);
        }

        //rbPlayer.velocity = new Vector2(moveHorizontal * speed, currentYVelocity);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Saltar();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            coyoteTimeCounter = 0;
        }

        if (rbPlayer.velocity.y < 0)
        {
            rbPlayer.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }
    }

    void GiroApuntado()
    {
        if (aim.apuntado < 90f && aim.apuntado > -90f)
        {
            //sprite.flipX = false;
            transform.localScale = new Vector2(1, 1);
        }

        /*if (aim.apuntado > 90f && aim.apuntado < -90f)
        {
            sprite.flipX = true;
            //transform.localScale = new Vector2(1, 1);
        }*/
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //sprite.flipX = true;
        transform.localScale = new Vector2(-1, 1);
    }

    public void Saltar()
    {
        //salto
        if (coyoteTimeCounter > 0f)
        {
            AudioController.Instance.PlaySFX("Salto");
            isWalking = false;

            isOnGround = false;
            //rbPlayer.velocity += new Vector2(rbPlayer.velocity.x, jumpForce);
            rbPlayer.AddForce(new Vector2(rbPlayer.velocity.x, jumpForce), ForceMode2D.Impulse);
            animatorPlayer.SetBool("Jumping", true);

        }
    }

    public void UpdatedLifeBar(int life)
    {
        vidas[life].SetActive(false);
    }

    public void PlayAudioClip(string s)
    {
        AudioController.Instance.PlaySFX(s);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Trap")
        {
            isOnGround = true;

            aim.impulse = false;

            animatorPlayer.SetBool("Jumping", false);

            if (!isWalking)
            {
                AudioController.Instance.PlaySFX("Caida");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = false;

            animatorPlayer.SetBool("Jumping", true);
        }
    }
}
