using UnityEngine;

public class ZRotation : MonoBehaviour
{
    public float rotationSpeed = 30f;       // The speed at which the object rotates

    private void Update()
    {
        transform.rotation *= Quaternion.Euler(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}