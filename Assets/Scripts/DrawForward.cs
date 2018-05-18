using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawForward : MonoBehaviour {

	void Update () {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 5);
	}
}
