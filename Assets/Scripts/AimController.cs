using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.VisualScripting;
using TarodevController;

public class AimController : MonoBehaviour
{
    public float impulseForce;

    public float apuntado;

    public Animator escopeta;

    public Rigidbody2D rb;

    public Transform muzzle;

    public float cooldownTime;
    bool isCooldown = false;

    public Animator shootAnim;

    private Transform aimTransform;
    public Transform playerTransform;

    public Vector3 angulo;

    private void Awake()
    {
        aimTransform = transform;

    }

    // Update is called once per frame
    void Update()
    {
        MovimientoArma();
        Shooting();
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
        if (Input.GetMouseButtonDown(0) && !isCooldown)
        {
            StartCoroutine(Cooldown());
            ImpulsoDisparo();
            //Impulso2();
            shootAnim.SetTrigger("Shoot");
            escopeta.SetTrigger("Shoot");
        }
    }

    void Impulso2()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 knockbackDirection = (transform.position - mousePosition).normalized;

        rb.velocity = knockbackDirection * impulseForce;
    }

    public void ImpulsoDisparo()
    {
        Vector3 mousePos = UtilsClass.GetMouseWorldPosition();
        mousePos.z = 0;

        rb.velocity = new Vector3(-mousePos.x, -mousePos.y, 0) * impulseForce * .3f;

        //rb.AddForce(new Vector2(-mousePos.x, -mousePos.y), ForceMode2D.Impulse);
    }

    public IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

}

