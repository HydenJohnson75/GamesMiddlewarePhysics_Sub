using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private GameObject actionObject;
    public GameObject actionObjectPrefab;
    private List<Sphere_Physics> randomGenedBalls;
    private float timer = 2f;
    private void Start()
    {

        randomGenedBalls= new List<Sphere_Physics>();

        //Vector3(-11.3599997, 2.71000004, -9.40999985)
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Physics");
    }

    private void Update()
    {

        if(randomGenedBalls ==null || randomGenedBalls.Count < 7)
        {
            if(timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                int randomNumber = UnityEngine.Random.Range(1, 3);

                switch (randomNumber)
                {
                    case 1:
                    {
                            Vector3 spawnPosition = new Vector3(-11.3599997f, UnityEngine.Random.Range(0f, 9), 0f);
                            actionObject = Instantiate(actionObjectPrefab, spawnPosition, Quaternion.identity);
                            Sphere_Physics ball = actionObject.ConvertTo<Sphere_Physics>();

                            ball.Velocity += new Vector3(10, 0, 0);

                            ball.assignRadius = UnityEngine.Random.Range(0.1f, 1);
                            if (ball.radius >= 0.2f)
                            {
                                ball.mass = UnityEngine.Random.Range(5f, 10f);
                            }
                            else
                            {
                                ball.mass = UnityEngine.Random.Range(0f, 4.9f);
                            }
                            randomGenedBalls.Add(ball);
                            timer = 3f;
                            break;
                    }
                    case 2:
                    {
                            Vector3 spawnPosition = new Vector3(11.3599997f, UnityEngine.Random.Range(0f, 9), 0f);
                            actionObject = Instantiate(actionObjectPrefab, spawnPosition, Quaternion.identity);
                            Sphere_Physics ball = actionObject.ConvertTo<Sphere_Physics>();

                            ball.Velocity += new Vector3(10, 0, 0);

                            ball.assignRadius = UnityEngine.Random.Range(0.1f, 1);
                            if(ball.radius >=0.2f)
                            {
                                ball.mass = UnityEngine.Random.Range(5f, 10f);
                            }
                            else
                            {
                                ball.mass = UnityEngine.Random.Range(0f, 4.9f);
                            }
                            randomGenedBalls.Add(ball);
                            timer = 3f;
                            break;
                    }
                    default: 
                    {

                            break;    
                    }
                }

                        
            }
            
        }
        
    }
}
