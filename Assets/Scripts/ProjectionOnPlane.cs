using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionOnPlane : MonoBehaviour {

    public GameObject cube;
    public GameObject light;
    public GameObject plane;
    public bool isShowingVectorComponents;

    public void Update()
    {
        float deltaY = cube.transform.position.y - plane.transform.position.y;
        Vector3 delta = Vector3.up * deltaY;
        Vector3 lightProjectedOntoVector = Vector3.Project(light.transform.forward, delta);
        float k = deltaY / lightProjectedOntoVector.magnitude;
        Vector3 castVector = light.transform.forward * k;
        Debug.DrawLine(cube.transform.position, cube.transform.position + castVector);

        // Cast castVector onto plane
        if (isShowingVectorComponents)
        {
            Vector3 projectionVector = Vector3.Project(castVector, plane.transform.up);
            Vector3 rejectionVector = castVector - projectionVector;
            Debug.DrawLine(cube.transform.position, cube.transform.position + projectionVector);
            Debug.DrawLine(cube.transform.position, cube.transform.position + rejectionVector);
        }
    }
}
