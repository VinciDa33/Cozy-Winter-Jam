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
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        int currentSize = other.gameObject.GetComponent<PlayerInterface>().getSize();
        if (currentSize == 1)
            Instantiate(mediumPlayer, transform.position, quaternion.identity);
        if (currentSize == 2)
            Instantiate(largePlayer, transform.position, quaternion.identity);
        if (currentSize == 3)
            Instantiate(completePlayer, transform.position, quaternion.identity);
        
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
