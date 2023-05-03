using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float speed = 1f;            // The speed at which the object moves
    public float distance = 0.2f;       // The distance the object moves back and forth
    public float delay = 2f;            // The delay in seconds before the object starts moving

    private float initialPosition;      // The initial position of the object
    public float direction = 1f;       // The direction the object is moving (-1 for left, 1 for right)

    private void Start()
    {
        initialPosition = transform.position.x;
        Invoke("StartMoving", delay);
    }

    private void StartMoving()
    {
        enabled = true;
    }

    private void Update()
    {
        float newPosition = transform.position.x + speed * direction * Time.deltaTime;
        if (Mathf.Abs(newPosition - initialPosition) > distance)
        {
            direction = -direction;
        }
        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
    }
}