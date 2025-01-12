using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Default offset
    public float followSpeed = 5f;
    public float forwardLeanFactor = 1f; // Factor to control how much the camera leans forward

    void LateUpdate()
    {
        if (target == null) return;

        // Get the target's velocity to detect movement direction
        Rigidbody targetRigidbody = target.GetComponent<Rigidbody>();
        if (targetRigidbody != null)
        {
            // Adjust the Z offset based on the forward speed of the target
            float speed = targetRigidbody.linearVelocity.magnitude;
            offset.z = -10f - (speed * forwardLeanFactor); // Reduce the Z offset as speed increases (camera leans forward)
        }

        // Calculate desired position based on offset
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}
