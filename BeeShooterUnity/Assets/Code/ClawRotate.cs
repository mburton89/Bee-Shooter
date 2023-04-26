using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawRotate : MonoBehaviour
{
    public float rotationSpeed;
    public float moveTime = 2f; // the amount of time the object will move in one direction
    private float timer = 0f; // a timer to track the movement time

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);

        if (timer <= 0f) // if the movement time has elapsed
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime); // switch the direction of movement
            timer = moveTime; // reset the timer
        }
    }
}
