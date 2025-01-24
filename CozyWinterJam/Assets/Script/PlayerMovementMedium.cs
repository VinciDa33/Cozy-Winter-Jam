using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementMedium : MonoBehaviour, PlayerInterface
{
    private Rigidbody2D rb;
    
    [SerializeField] private float accelereation;
    [SerializeField] private float deceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        if (input != 0 && Mathf.Abs(rb.velocity.x) < maxSpeed)
            rb.velocity += new Vector2(input * accelereation * Time.deltaTime, 0);
        else
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0f, deceleration * Time.deltaTime), rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            if (Physics2D.Raycast(transform.position + new Vector3(0.35f, 0f, 0f), Vector2.down, groundCheckDistance, groundLayer) || Physics2D.Raycast(transform.position - new Vector3(0.35f, 0f, 0f), Vector2.down, groundCheckDistance, groundLayer))
                rb.velocity += new Vector2(0f, jumpForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(0.35f, 0f, 0f), transform.position + new Vector3(0.35f, 0f, 0f) + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(transform.position - new Vector3(0.35f, 0f, 0f), transform.position - new Vector3(0.35f, 0f, 0f) + Vector3.down * groundCheckDistance);
    }

    public int getSize()
    {
        return 2;
    }
}
