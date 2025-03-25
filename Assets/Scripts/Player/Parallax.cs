using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    ParallaxActivator activator;

    private Vector3 previousCameraPosition;
    private Transform cameraTransform;

    public float distance;

    public GameObject camara;

    // Start is called before the first frame update
    void Start()
    {
        activator = GetComponentInParent<ParallaxActivator>();
        cameraTransform = camara.transform;
        previousCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (activator.playerNear)
        {
            float deltaX = (cameraTransform.position.x - previousCameraPosition.x) * distance;
            transform.Translate(new Vector3(deltaX, 0, 0));
            previousCameraPosition = cameraTransform.position;
        }
    }
}
