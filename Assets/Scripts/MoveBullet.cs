using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    private Rigidbody bullet;
    [SerializeField]private float bulletSpeed = 0.4f;

    [SerializeField] private float ttl = 5f;

    private void Awake()
    {
        bullet = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveBala();
    }


    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolCoroutine());
    }

    IEnumerator ReturnToPoolCoroutine()
    {
        yield return new WaitForSeconds(ttl);
        PoolBalas.Instance.Return(gameObject);
    }

    void moveBala()
    {
        transform.Translate(Vector2.up*bulletSpeed*Time.deltaTime,Space.World);
       //bullet.AddForce(transform.up * bulletSpeed*Time.deltaTime, ForceMode.Impulse);
    }
}
