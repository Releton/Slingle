using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

    public float sensitivity;
    private float currentYRotation = 90f;
    private float rotationSpeed;
    private void Update()
    {
        float rotationAmount = 0f;
        rotationSpeed = Input.GetKey(KeyCode.LeftShift) ? sensitivity * 1.5f : sensitivity;
        // Check input and calculate rotation
        if (Input.GetKey(KeyCode.A))
        {
            rotationAmount = -rotationSpeed * Time.deltaTime; // Rotate left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotationAmount = rotationSpeed * Time.deltaTime; // Rotate right
        }

        // Update current rotation and clamp it
        currentYRotation += rotationAmount;
        currentYRotation = Mathf.Clamp(currentYRotation, 0f, 180f);

        // Apply the clamped rotation
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentYRotation, transform.eulerAngles.z);
    }
}
