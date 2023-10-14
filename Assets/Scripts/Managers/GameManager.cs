using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject target;
    public GameObject spawnedCoin;
    private UnityEngine.SceneManagement.Scene loadedScene;
    private float timer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        loadedScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(loadedScene.name == "Physics")
        {
            if(timer >= 0)
            {
                if(spawnedCoin == null)
                {
                    Vector3 spawnPosition = new Vector3(-9.98200035f, Random.Range(1.23000002f, 3.61299992f), Random.Range(2.44700003f, 2.44700003f));
                    spawnedCoin = Instantiate(target, spawnPosition, Quaternion.Euler(90f, 90f, 0f));
                    
                }
                timer -= Time.deltaTime;
            }
            else
            {
                Destroy(spawnedCoin);
                timer = 3f;
            }
            
        }
        

        
    }
}
