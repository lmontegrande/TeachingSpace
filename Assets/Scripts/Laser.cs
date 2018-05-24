using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public int maxBounces = 10;

    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        int bounces = 0;

        _lineRenderer.positionCount = maxBounces + 1;
        _lineRenderer.SetPosition(0, transform.position);
        int rayPosition = 1;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        while (true)
        {
            _lineRenderer.positionCount = rayPosition + 1;
            if (bounces >= maxBounces)
                break;
            else
                bounces++;

            if (Physics.Raycast(ray, out hit, 1000f))
            {
                ray.origin = hit.point;
                ray.direction = ray.direction - (2 * Vector3.Project(ray.direction, hit.normal));
                _lineRenderer.SetPosition(rayPosition, hit.point);
                rayPosition++;
            }
            else
            {
                _lineRenderer.SetPosition(rayPosition, ray.origin + (1000f * ray.direction));
                break;
            }
        }
    }
}
