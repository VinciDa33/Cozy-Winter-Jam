using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SnowPile : MonoBehaviour
{
    [SerializeField] private GameObject mediumPlayer;
    [SerializeField] private GameObject largePlayer;
    [SerializeField] private GameObject completePlayer;

    [Header("Effects")] [SerializeField] private GameObject particleEffect;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        
        int currentSize = other.gameObject.GetComponent<PlayerInterface>().getSize();
        if (currentSize == 1)
            Instantiate(mediumPlayer, transform.position, quaternion.identity);
        if (currentSize == 2)
            Instantiate(largePlayer, transform.position, quaternion.identity);
        if (currentSize == 3)
            Instantiate(completePlayer, transform.position, quaternion.identity);

        Instantiate(particleEffect, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
