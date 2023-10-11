using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere_Physics : Interactable, I_Collidable
{

    [SerializeField] private Vector3 velocity;
    [SerializeField] internal float assignRadius;
    Vector3 acceleration;
    [SerializeField] internal float mass;
    public float cor = 0.6f;
    public Vector3 previousPosition;

    internal float radius { get { return transform.localScale.x / 2f; } set { transform.localScale = 2 * value * Vector3.one; } }

    public Vector3 Velocity { get { return velocity; } set { velocity = value; } }
    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }

    // Start is called before the first frame update
    void Start()
    {
        acceleration = 9.8f * Vector3.down;
        radius = assignRadius;
    }

    // Update is called once per frame
    void Update()
    {
        previousPosition = transform.position;

        velocity += acceleration * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;

    }

    public bool IsColliding(I_Collidable otherObject)
    {

        float distance; 

        switch (otherObject)
        {
            case Sphere_Physics otherSphere:
                {
                    distance = Vector3.Distance(transform.position, otherObject.Position);
                    return distance < radius + otherSphere.radius;
                }
            case PlaneScript otherPlane:
                {

                    Vector3 v = transform.position - otherPlane.transform.position;

                    Vector3 norm = otherPlane.transform.up;

                    distance = PhysicsManager.getParallel(v, norm).magnitude;

                    return distance < radius;
                }
                default: return false;

        }


        //return distance < radius + otherSphere.radius;
    }

    public (Vector3, Vector3) ResolveCollisionWithOther(I_Collidable otherObject)
    {
        switch (otherObject)
        {
            case Sphere_Physics otherSphere:
                return ResolveCollisionWithSphere(otherSphere);

            case PlaneScript otherPlane:

                ResolveCollisionWithPlane(otherPlane);
                return (Vector3.zero, otherPlane.Position);

            default:
                return (Vector3.zero, Vector3.zero);
        }
    }

    public (Vector3, Vector3) ResolveCollisionWithSphere(Sphere_Physics otherSphere)
    {
        Vector3 otherSpherePosition, otherSphereVeocity;


        float d0 = (Vector3.Distance(previousPosition, otherSphere.previousPosition)) - radius - otherSphere.radius;
        float d1 = (Vector3.Distance(transform.position, otherSphere.Position)) - radius - otherSphere.radius;
        Debug.Log(d0);
        //Debug.Log(d1);

        float ToI = d1 * (Time.deltaTime / (d1 - d0));


        Vector3 thisMovePosition = transform.position - Velocity * ToI;
        Vector3 otherSphereMovePosition = otherSphere.Position - otherSphere.Velocity * ToI;

        Vector3 thisCollideVelocity = Velocity - acceleration * ToI;
        Vector3 otherSphereCollideVelocity = otherSphere.Velocity - otherSphere.acceleration * ToI;

        Vector3 normal = (thisMovePosition - otherSphereMovePosition).normalized;
        Vector3 u1 = PhysicsManager.getParallel(thisCollideVelocity, normal);
        Vector3 u2 = PhysicsManager.getParallel(otherSphereCollideVelocity, normal);
        Vector3 s1 = PhysicsManager.getPerpendicular(thisCollideVelocity, normal);
        Vector3 s2 = PhysicsManager.getPerpendicular(otherSphereCollideVelocity, normal);

        float m1 = mass;
        float m2 = otherSphere.mass;

        Vector3 v1 = ((m1 - m2) / (m1 + m2)) * u1 + ((2 * m2) / (m1 + m2)) * u2;

        Vector3 v2 = ((2 * m1) / (m1 + m2)) * u1 + ((m2 - m1) / (m1 + m2)) * u2;

        v1 *= cor;
        v2 *= otherSphere.cor;

        v1 += acceleration * ToI;
        v2 += otherSphere.acceleration * ToI;

        Velocity = v1 + s1;
        otherSphereVeocity = v2 + s2;

        transform.position = thisMovePosition + Velocity * ToI;
        otherSpherePosition = otherSphereMovePosition + otherSphereVeocity * ToI;

        return (otherSphereVeocity, otherSpherePosition);
    }

    public void ResolveCollisionWithPlane(PlaneScript plane)
    {
        Vector3 norm = plane.transform.up;

        Vector3 v = velocity * Time.deltaTime;

        transform.position += PhysicsManager.getPerpendicular(v, norm) - PhysicsManager.getParallel(v, norm);


        velocity = (PhysicsManager.getPerpendicular(velocity, norm) - (cor * PhysicsManager.getParallel(velocity, norm)));
    }

    protected override void Interact()
    {

    }

}
