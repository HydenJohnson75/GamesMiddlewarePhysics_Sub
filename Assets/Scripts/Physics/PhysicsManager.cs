using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{


    List<I_Collidable> collidableObjects;

    void Start()
    {
        collidableObjects = FindObjectsOfType<MonoBehaviour>().OfType<I_Collidable>().ToList();

    }


    private void Update()
    {
        collidableObjects = FindObjectsOfType<MonoBehaviour>().OfType<I_Collidable>().ToList();
    }

    void LateUpdate()
    {

        for (int i = 0; i < collidableObjects.Count - 1; i++)
        {
            
            for (int j = i + 1; j < collidableObjects.Count; j++)
           {


                I_Collidable firstCollidableObject = collidableObjects[i];
                I_Collidable secondCollidableObject = collidableObjects[j];

                if (firstCollidableObject.IsColliding(secondCollidableObject))
                {
                    (secondCollidableObject.Velocity, secondCollidableObject.Position) = firstCollidableObject.ResolveCollisionWithOther(secondCollidableObject);
                }
                else if(secondCollidableObject.IsColliding(firstCollidableObject))
                {
                    (firstCollidableObject.Velocity, firstCollidableObject.Position) = secondCollidableObject.ResolveCollisionWithOther(firstCollidableObject);
                }

            } 
        }
    }

    public static Vector3 getParallel(Vector3 smallV, Vector3 norm)
    {
        Vector3 parallel = Vector3.Dot(smallV, norm) * norm;

        return parallel;
    }

    public static Vector3 getPerpendicular(Vector3 smallV, Vector3 norm)
    {
        Vector3 perpendicular = smallV - getParallel(smallV, norm);
        return perpendicular;
    }

}