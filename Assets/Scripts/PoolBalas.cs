using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBalas : MonoBehaviour
{
    public static PoolBalas Instance { get; private set; }
    [SerializeField]private GameObject balasPrefab;
    [SerializeField] private int initialAmount = 50;

    private List<GameObject> pool = new List<GameObject>();
    
    private void Awake()
    {
        Instance = this;
        FillPool();
    }

    private void FillPool()
    {
        for (int i = 0; i < initialAmount; i++)
        {
            GameObject gO = Instantiate(balasPrefab);
            gO.SetActive(false);
            pool.Add(gO);
        }
    }



    public GameObject Get()
    {
        GameObject ret;
        if (pool.Count > 0)
        {
            ret = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count-1);
        }
        else
        {
            ret = Instantiate(balasPrefab);
        }
        
        ret.SetActive(true);
        return ret;

    }

    public void Return(GameObject gO)
    {
        gO.SetActive(false);
        pool.Add(gO);
    }
    
}

