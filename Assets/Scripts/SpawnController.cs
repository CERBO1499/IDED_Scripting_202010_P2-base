using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnObjects;

    [SerializeField]
    private float spawnRate = 1f;

    [SerializeField]
    private float firstSpawnDelay = 0f;

    [SerializeField]
    private float timer;
    [SerializeField]
    private float timeBetwenSpawns=2f;

    private Vector3 spawnPoint;

    [SerializeField] private float xValue;
    [SerializeField] private float xValue2; 
 
    private PoolHazard pool;

    private bool IsThereAtLeastOneObjectToSpawn
    {
        get
        {
            bool result = false;

            for (int i = 0; i < spawnObjects.Length; i++)
            {
                result = spawnObjects[i] != null;

                if (result)
                {
                    break;
                }
            }

            return result;
        }
    }

   /* private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetwenSpawns)
        {
            SpawnObject(spawnObjects.Length);
            timer = 0;
        }
        
        
        
    }*/

    // Start is called before the first frame update
    private void Start()
    {

        pool = GetComponent<PoolHazard>();
        if (spawnObjects.Length > 0 && IsThereAtLeastOneObjectToSpawn)
        {
           InvokeRepeating("SpawnObject", firstSpawnDelay, spawnRate);

            if (Player._instance != null)
            {
                Player._instance.OnPlayerDied += StopSpawning;
            }
        }
    }

    private void SpawnObject()
    {
        
       

        GameObject spawnGO = pool.Allocate(Random.Range(0,spawnObjects.Length));
        
        
        
        //spawnGO = spawnObjects[Random.Range(0, spawnObjects.Length)];
        
        //GameObject spawnGO = spawnObjects[Random.Range(0, spawnObjects.Length)];

        if (spawnGO != null)
        {
            /*spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(
                Random.Range(0F, 1F), 1F, transform.position.z));*/ 
            spawnGO.transform.position = transform.position + transform.right * Random.Range(xValue,xValue2); 
            spawnGO.SetActive(true);
            //GameObject instance = Instantiate(spawnGO, spawnPoint, Quaternion.identity);
        }
    }

    private void StopSpawning()
    {
        CancelInvoke();
    }
}