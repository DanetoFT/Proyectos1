using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Vector3 previousCameraPosition;
    private Transform cameraTransform;

    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - previousCameraPosition.x) * distance;
        transform.Translate(new Vector3 (deltaX, 0, 0));
        previousCameraPosition = cameraTransform.position;
    }
}
