using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PlaneScript : MonoBehaviour, I_Collidable
{
    public Vector3 Velocity { get { return Vector3.zero; } set { } }
    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }

    public bool IsColliding(I_Collidable otherObject)
    {
        switch (otherObject)
        {
            default:
                return false;
        }
    }

    public (Vector3, Vector3) ResolveCollisionWithOther(I_Collidable otherObject)
    {
        switch (otherObject)
        {
            default:
                return (Vector3.zero, Vector3.zero);
        }
    }
}
