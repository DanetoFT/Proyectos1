using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.VisualScripting;
using System.ComponentModel;

public class AimController : MonoBehaviour
{

    public float impulseForce;

    public float apuntado;

    public Animator escopeta;

    public Rigidbody2D rb;

    public Transform muzzle;

    public float cooldownTime;
    bool isCooldown = false;
    public bool impulse;

    public Animator shootAnim;

    private Transform aimTransform;
    public Transform playerTransform;
    public Transform shootPoint;
    public float rango = 10f;
    public GameObject bullet;

    public bool stand;

    SpriteRenderer sprite;

    public PlayerController player;

    public Vector3 angulo;

    private void Awake()
    {
        impulse = false;
        aimTransform = transform;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.isPaused)
        {
            MovimientoArma();
            Shooting();
        }
    }

    public void MovimientoArma()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        angulo = aimDirection;

        apuntado = angle;

        if(angle > 90)
        {
            transform.localScale = new Vector2(1, -1);
        }
        else if (angle < -90)
        {
            transform.localScale = new Vector2(1, -1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }

        transform.position = playerTransform.position;
    }

    void Shooting()
    {
        if (Input.GetMouseButtonDown(0) && !isCooldown && !player.isOnGround)
        {
            StartCoroutine(Cooldown());

            impulse = true;

            AudioController.Instance.PlaySFX("Shoot");

            Instantiate(bullet, shootPoint.position, Quaternion.identity);

            //ImpulsoDisparo();
            Impulso2();
            shootAnim.SetTrigger("Shoot");
            escopeta.SetTrigger("Shoot");
        }
        else if (Input.GetMouseButtonDown(0) && !isCooldown && player.isOnGround)
        {
            StartCoroutine(Cooldown());

            AudioController.Instance.PlaySFX("Shoot");

            Instantiate(bullet, shootPoint.position, Quaternion.identity);

            shootAnim.SetTrigger("Shoot");
            escopeta.SetTrigger("Shoot");
        }
    }

    void Impulso2()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 mouseRotation = mousePosition - transform.position;

        Vector2 knockbackDirection = (transform.position - mousePosition).normalized;

        Debug.Log(knockbackDirection);

        rb.velocity += knockbackDirection * impulseForce;

        //rb.AddForce(knockbackDirection * 50, ForceMode2D.Impulse);
    }

    public void ImpulsoDisparo()
    {
        Vector3 mousePos = UtilsClass.GetMouseWorldPosition();
        mousePos.z = 0;

        rb.velocity = new Vector3(-mousePos.x, -mousePos.y, 0) * impulseForce * .3f;

        //rb.AddForce(new Vector2(-mousePos.x, -mousePos.y) * 50, ForceMode2D.Impulse);
    }

    public IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
        impulse = false;
    }

}

