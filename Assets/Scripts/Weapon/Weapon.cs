using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform barrelLocation;
    public GameObject spherePrefab;
    private GameObject spawnedSphere;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void shoot()
    {
        Vector3 spawnPosition = barrelLocation.position;
        spawnedSphere = Instantiate(spherePrefab, spawnPosition, spherePrefab.transform.rotation);
        Sphere_Physics ball = spawnedSphere.ConvertTo<Sphere_Physics>();

        ball.assignRadius = 0.1f;
        ball.mass = 0.1f;
        ball.cor = 0.5f;
        ball.Velocity += barrelLocation.right * 20;

        StartCoroutine(DestroyAfterDelay(3f, ball)); 
    }

    private IEnumerator DestroyAfterDelay(float delay, Sphere_Physics spawnedBall)
    {
        yield return new WaitForSeconds(delay);
        GameObject spawnedObject = spawnedBall.gameObject;
        if (spawnedObject != null)
        {
            Destroy(spawnedObject); 
        }
    }
}
