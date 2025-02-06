using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.VisualScripting;
using TarodevController;

public class AimController : MonoBehaviour
{
    [SerializeField] private ScriptableStats _stats;

    public Rigidbody2D rb;

    NewPlayerController player;

    public Vector3 muzzle;

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

        muzzle = UtilsClass.GetMouseWorldPosition(); ;

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

            shootAnim.SetTrigger("Shoot");
        }
    }

    public void ImpulsoDisparo()
    {
        Vector2 direction = (-muzzle + transform.position);

        Vector2 knockback = direction * 1000f;

        rb.AddForce(knockback, ForceMode2D.Force);
    }

    public IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

}

