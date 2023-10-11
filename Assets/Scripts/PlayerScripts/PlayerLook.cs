using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotration = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;


    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotration -=(mouseY * Time.deltaTime) * ySensitivity;
        xRotration = Mathf.Clamp(xRotration, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRotration, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

}
