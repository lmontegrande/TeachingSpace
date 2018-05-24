using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTransformation : MonoBehaviour {

    public GameObject[] objects;
    private Matrix4x4 savedTranformations {
        get
        {
            return _savedTransformation;
        }
        set
        {
            Debug.Log("saved transformation set to: \n" + _savedTransformation);
            _savedTransformation = value;
        }
    }
    private Matrix4x4 _savedTransformation = Matrix4x4.identity;

    public void Update()
    {
        Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (inputAxis.magnitude >= 0.01f)
        {
            Matrix4x4 translateMatrix = new Matrix4x4(
            new Vector4(1f, 0, 0, 0),
            new Vector4(0, 1f, 0, 0),
            new Vector4(0, 0, 1f, 0),
            new Vector4(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"), 1f));

            savedTranformations *= translateMatrix;

            foreach (GameObject obj in objects)
            {
                Vector3 position = obj.transform.position;
                Vector4 newPosition = savedTranformations * translateMatrix * savedTranformations.inverse * new Vector4(position.x, position.y, position.z, 1);
                obj.transform.position = newPosition;
            }
        }
    }

    public void Transform(bool isInverse)
    {
        float theta = 45f;
        float thetaAsRadians = theta * Mathf.Deg2Rad;

        Matrix4x4 rotationXMatrix = new Matrix4x4(
            new Vector4(1f, 0, 0, 0),
            new Vector4(0, Mathf.Cos(thetaAsRadians), Mathf.Sin(thetaAsRadians), 0),
            new Vector4(0, -Mathf.Sin(thetaAsRadians), Mathf.Cos(thetaAsRadians), 0),
            new Vector4(0, 0, 0, 1f)
            );
        
        Matrix4x4 rotationZMatrix = new Matrix4x4(
            new Vector4(Mathf.Cos(thetaAsRadians), Mathf.Sin(thetaAsRadians), 0, 0),
            new Vector4(-Mathf.Sin(thetaAsRadians), Mathf.Cos(thetaAsRadians), 0, 0),
            new Vector4(0, 0, 1f, 0),
            new Vector4(0, 0, 0, 1f));
        
        Matrix4x4 rotationYMatrix = new Matrix4x4(
            new Vector4(Mathf.Cos(thetaAsRadians), 0, -Mathf.Sin(thetaAsRadians), 0),
            new Vector4(0, 1f, 0, 0),
            new Vector4(Mathf.Sin(thetaAsRadians), 0, Mathf.Cos(thetaAsRadians), 0),
            new Vector4(0, 0, 0, 1f));

        Matrix4x4 scaleMatrix = new Matrix4x4(
            new Vector4(2f, 0, 0, 0),
            new Vector4(0, 2f, 0, 0),
            new Vector4(0, 0, 2f, 0),
            new Vector4(0, 0, 0, 1f));

        Matrix4x4 translateMatrix = new Matrix4x4(
            new Vector4(1f, 0, 0, 0),
            new Vector4(0, 1f, 0, 0),
            new Vector4(0, 0, 1f, 0),
            new Vector4(2f, 0f, 0f, 1f)); // x, y, z

        Matrix4x4 transformationMatrix = rotationYMatrix;
        transformationMatrix = isInverse ? transformationMatrix.inverse : transformationMatrix;
        savedTranformations *= transformationMatrix;

        foreach (GameObject obj in objects)
        {
            Vector3 position = obj.transform.position;
            Vector4 newPosition = transformationMatrix * new Vector4(position.x, position.y, position.z, 1);
            //Vector4 newPosition = savedTranformations * transformationMatrix * savedTranformations.inverse * new Vector4(position.x, position.y, position.z, 1);
            Debug.DrawLine(obj.transform.position, newPosition, Color.red, 1f);
            obj.transform.position = newPosition;
        }
    }

    public void Reset()
    {
        foreach(GameObject obj in objects)
        {
            Vector3 position = obj.transform.position;
            Vector4 newPosition = savedTranformations.inverse * new Vector4(position.x, position.y, position.z, 1);
            obj.transform.position = newPosition;
        }
        savedTranformations *= savedTranformations.inverse;
    }   
}