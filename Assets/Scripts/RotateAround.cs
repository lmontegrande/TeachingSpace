using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

    public GameObject sun;
    public GameObject planet;
    public GameObject cam;
    public float rotateSpeed = 5f;
    public float moveSpeed = 2f;
    public bool isUsingRelativeRotation;

    private float mouseDeltaX;
    private float previousMouseX;

    private void Update()
    {
        mouseDeltaX = Input.mousePosition.x - previousMouseX;
        previousMouseX = Input.mousePosition.x;
        HandleOrbit();
        HandleMovement();
    }

    private void HandleOrbit()
    {
        float distance = (sun.transform.position - planet.transform.position).magnitude;
        Vector3 crossProduct = Vector3.Cross(planet.transform.position - sun.transform.position, Vector3.up);
        planet.transform.position += crossProduct.normalized * rotateSpeed * Time.deltaTime * mouseDeltaX;
        cam.transform.LookAt(sun.transform.position);

        Vector3 delta = (planet.transform.position - sun.transform.position).normalized;
        planet.transform.position = sun.transform.position + (delta * distance);
    }

    private void HandleMovement()
    {
        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (!isUsingRelativeRotation)
        {
            sun.transform.position += Vector3.forward * inputVector.y * Time.deltaTime * moveSpeed;
            sun.transform.position += Vector3.right * inputVector.x * Time.deltaTime * moveSpeed;
        }
        else
        {
            Vector3 projectedForward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up);
            sun.transform.position += projectedForward * moveSpeed * inputVector.y * Time.deltaTime;
            Vector3 crossVector = -Vector3.Cross(projectedForward, Vector3.up);
            sun.transform.position += crossVector * moveSpeed * inputVector.x * Time.deltaTime;
        }
    }
}
