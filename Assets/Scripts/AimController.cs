using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class AimController : MonoBehaviour
{
    private Transform aimTransform;
    public Transform playerTransform;


    private void Awake()
    {
        aimTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoArma();
    }

    public void MovimientoArma()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

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

}

