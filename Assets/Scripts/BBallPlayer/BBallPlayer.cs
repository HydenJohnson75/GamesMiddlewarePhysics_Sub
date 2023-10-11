using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BBallPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 3f;
    public float minYAngle = -80f; // Minimum camera Y angle.
    public float maxYAngle = 80f;  // Maximum camera Y angle.
    public Transform cameraTransform;
    public GameObject actionObjectPrefab;

    private Rigidbody rb;
    private GameObject actionObject;
    private float currentRotationX = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Camera rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        currentRotationX -= mouseY * rotationSpeed;
        currentRotationX = Mathf.Clamp(currentRotationX, minYAngle, maxYAngle);

        cameraTransform.localRotation = Quaternion.Euler(currentRotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX * rotationSpeed);

        // Character movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        Vector3 moveVelocity = transform.TransformDirection(moveDirection) * moveSpeed;
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

        // Left-click action
        if (Input.GetMouseButtonDown(0))
        {
            PerformAction();
        }
    }

    private void PerformAction()
    {
            Vector3 spawnPosition = cameraTransform.transform.position + cameraTransform.forward * 2f;
            actionObject = Instantiate(actionObjectPrefab, spawnPosition, Quaternion.identity);
            Sphere_Physics ball = actionObject.ConvertTo<Sphere_Physics>();

            ball.Velocity += cameraTransform.forward *5;
    }
}
