using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 3f;
    public float minYAngle = -80f;
    public float maxYAngle = 80f; 
    public Transform cameraTransform;
    public GameObject actionObjectPrefab;

    private Rigidbody rb;
    private GameObject actionObject;
    private float currentRotationX = 0f;
    private Weapon gun;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gun = FindObjectOfType<Weapon>();
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        currentRotationX -= mouseY * rotationSpeed;
        currentRotationX = Mathf.Clamp(currentRotationX, minYAngle, maxYAngle);

        cameraTransform.localRotation = Quaternion.Euler(currentRotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX * rotationSpeed);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        Vector3 moveVelocity = transform.TransformDirection(moveDirection) * moveSpeed;
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

        if (Input.GetMouseButtonDown(0))
        {
            ShootWeapon();
        }
    }

    private void ShootWeapon()
    {
        gun.shoot();
    }
}
