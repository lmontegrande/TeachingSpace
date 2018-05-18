using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projection : MonoBehaviour {

    public Vector3 point1;
    public Vector3 point2;
    public Vector3 origin = Vector3.zero;
    public bool offsetProjection;
    public bool drawConnectingLine;
    public float offsetDistance = .25f;

    private void Update()
    {
        DrawProjection();
    }

    private void DrawProjection()
    {
        float topDotProduct = (point1.x * point2.x) + (point1.y * point2.y) + (point1.z * point2.z);
        float bottomProduct = (point2.x * point2.x) + (point2.y * point2.y) + (point2.z * point2.z);
        Vector3 projectionVector = (topDotProduct / bottomProduct) * point2;

        Debug.DrawLine(origin, origin + point1, Color.red);
        Debug.DrawLine(origin, origin + point2, Color.blue);
        if (offsetProjection)
        {
            Vector3 offset = -Vector3.up * offsetDistance;
            Debug.DrawLine(offset + origin, offset + origin + projectionVector, Color.green);
            if (drawConnectingLine)
            {
                Debug.DrawLine(origin + point1, offset + origin + projectionVector);
            }
        }
        else
        {
            Debug.DrawLine(origin, origin + projectionVector, Color.green);
            if (drawConnectingLine)
            {
                Debug.DrawLine(origin + point1, origin + projectionVector);
            }
        }
    }
}
