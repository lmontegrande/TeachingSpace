using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    
    public float maxSpeed = 3f;
    public float jumpVelocity = 100f;
    public float collisionCheckDistance = 1.5f;
    public bool isSlideAlongWalls = false;
    public bool skew = false;

    private Rigidbody _rigidBody;
    private Vector3 velocity;
    private Vector2 axisInput;
    private bool isJumping;

    private float root2over2 {
        get { return Mathf.Sin(1 / 4f); }
    }

    private Matrix4x4 skewMatrix {
        get {
            Debug.Log(transform.rotation.z);
            float rotation = Quaternion.ToEulerAngles(transform.rotation).z;
            float sinRotation = Mathf.Sin(rotation);
            float cosRotation = Mathf.Cos(rotation);
            Vector4 vec1 = new Vector4(cosRotation, sinRotation, 0, 0);
            Vector4 vec2 = new Vector4(-sinRotation, cosRotation, 0, 0);
            Vector4 vec3 = new Vector4(0, 0, 1, 0);
            Vector4 vec4 = new Vector4(0, 0, 0, 1);
            Matrix4x4 skewMatrix = new Matrix4x4(vec1, vec2, vec3, vec4);
            return skewMatrix;
        }
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update () {
        UpdateValues();
        HandleInput();
        CalculateForces();
        if (isSlideAlongWalls)
            SlideAlongWalls();
        UpdatePosition();
	}

    private void UpdateValues()
    {
        velocity = skewMatrix.inverse * _rigidBody.velocity;
    }

    private void HandleInput()
    {
        axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        isJumping = Input.GetKeyDown(KeyCode.W);
    }

    private void CalculateForces()
    {
        // Add input speed
        velocity.x = axisInput.x * maxSpeed;
        velocity += Vector3.up * -9.8f * Time.deltaTime;
        if (isJumping)
        {
            velocity.y += jumpVelocity;
        }
    }

    private void SlideAlongWalls()
    {
        Vector3 castDirection = (transform.right * axisInput.x).normalized;
        Ray ray = new Ray(transform.position, castDirection);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, collisionCheckDistance))
        {
            velocity = Vector3.Project(velocity, Vector3.up);
        }

        Debug.DrawLine(transform.position, transform.position + (castDirection * collisionCheckDistance));
    }

    private void UpdatePosition()
    {
        if (skew)
        {
            velocity = skewMatrix * velocity;
        }
        _rigidBody.velocity = velocity;
        
        Debug.DrawLine(transform.position, transform.position + velocity);
    }
}
