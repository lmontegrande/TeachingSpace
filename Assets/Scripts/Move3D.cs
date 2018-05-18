using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3D : MonoBehaviour {

    public float speed;
    public float rotationalSpeed;

    private Rigidbody _rigidbody;
    private Vector2 inputVector;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // Handle Backstab
            Vector3 deltaVector = collision.gameObject.transform.position - transform.position;
            Vector3 enemyForward = collision.gameObject.transform.forward;
            float dotProduct = Vector3.Dot(deltaVector.normalized, enemyForward);
            Debug.Log("Dot: " + dotProduct);
            Debug.Log("Degrees: " + Mathf.Acos(dotProduct) * Mathf.Rad2Deg);
            if (dotProduct >= .75f)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    private void Update () {
        HandleInput();
        UpdateMovement();
	}

    private void HandleInput()
    {
        inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void UpdateMovement()
    {
        transform.Rotate(Vector3.up, inputVector.x * rotationalSpeed);
        _rigidbody.velocity = transform.forward * inputVector.y * speed;
    }
}
