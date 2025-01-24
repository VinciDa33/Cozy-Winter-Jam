using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSmall : MonoBehaviour, PlayerInterface
{
    private Rigidbody2D rb;
    
    [SerializeField] private float accelereation;
    [SerializeField] private float deceleration;
    [SerializeField] private float maxSpeed;

    [Header("Visual")] [SerializeField] private Transform face;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        if (input != 0 && Math.Abs(rb.velocity.x) < maxSpeed)
            rb.velocity += new Vector2(input * accelereation * Time.deltaTime, 0);
        else
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0f, deceleration * Time.deltaTime), rb.velocity.y);
        
        face.Rotate(Vector3.forward, -rb.velocity.x * 0.1f);
    }

    public int getSize()
    {
        return 1;
    }
}
