using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    private PlayerInput controls;
    private Camera cam;
    private RaycastHit rayHit;

    [SerializeField]
    private float bulletRange;
    [SerializeField]
    private float fireRate, reloadTime;
    [SerializeField]
    private bool isAutomatic;
    [SerializeField]
    private int magazineSize;
    private int ammoLeft;

    private bool isShooting, readyToShoot, reloading;

    public GameObject barrelExit;
    private void Awake()
    {
        controls = new PlayerInput();
        cam = Camera.main;

        controls.OnFoot.Shoot.started += ctx => StartShot();
        controls.OnFoot.Shoot.canceled += ctx => EndShot();
        controls.OnFoot.Reload.performed += ctx => Reload();
    }
    private void Update()
    {
        if (isShooting && readyToShoot && !reloading && ammoLeft >0)
        {
            PerformShot();
        }
    }

    private void StartShot()
    {
        isShooting = true;
    }

    private void EndShot()
    {

    }

    private void PerformShot()
    {
        readyToShoot = false;
        //Vector3 direction = cam.transform.forward;

        //if ((Physics.Raycast(cam.transform.position, direction, out rayHit, bulletRange)))
        //{

        //}

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject bullet = Instantiate(sphere, barrelExit.transform.position, barrelExit.transform.rotation);

        ammoLeft--;

        if(ammoLeft >= 0)
        {
            Invoke("ResetShot", fireRate);

            if (!isAutomatic)
            {
                EndShot();
            }
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinish", reloadTime);
    }

    private void ReloadFinish()
    {
        ammoLeft = magazineSize;
        reloading = false;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
