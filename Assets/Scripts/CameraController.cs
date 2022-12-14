using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 1.0f;

    private float rotationY;
    private float rotationX;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distanceFromTarget = 7f;

    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;

    [SerializeField]
    private float smoothTime = 0.1f;

    [SerializeField]
    private Vector2 rotationXMinMax = new Vector2(10, 50);

    void Update()
    {
        float mouseX = InputManager._INPUT_MANAGER.GetMouseX() * mouseSensitivity;
        float mouseY = InputManager._INPUT_MANAGER.GetMouseY() * mouseSensitivity;

        rotationY += mouseX;
        rotationX += mouseY;

        // Apply clamping for x rotation 
        rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);

        Vector3 nextRotation = new Vector3(rotationX, rotationY);

        // Apply damping between rotation changes
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
        transform.localEulerAngles = currentRotation;

        // Substract forward vector of the GameObject to point its forward vector to the target
        transform.position = target.position - transform.forward * distanceFromTarget;
    }
}
