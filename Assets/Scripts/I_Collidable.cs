using UnityEngine;

public interface I_Collidable
{
    Vector3 Velocity { get; set; }
    Vector3 Position { get; set; }

    public bool IsColliding(I_Collidable collidableObject);
    public (Vector3, Vector3) ResolveCollisionWithOther(I_Collidable collidableObject);
}
