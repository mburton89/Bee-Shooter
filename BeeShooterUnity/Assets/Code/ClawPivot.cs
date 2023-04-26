using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawPivot : MonoBehaviour
{
	public Transform pivot; // reference to the pivot transform
    public float speed = 5f; // the speed at which the object will move
    private void Update()
    {
        float verticalInput = Input.GetAxis("Vertical"); // get the input axis value
        transform.RotateAround(pivot.position, Vector3.right, verticalInput * speed * Time.deltaTime); // rotate the object around the pivot
    }
}
