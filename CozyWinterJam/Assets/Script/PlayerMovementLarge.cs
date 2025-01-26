using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementLarge : MonoBehaviour, PlayerInterface
{
    private Rigidbody2D rb;
    
    [Header("Movement")]
    [SerializeField] private float accelereation;
    [SerializeField] private float deceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;
    private bool isGrounded = true;
    private string[] sfx = {"SnowLand1", "SnowLand2", "SnowLand3"};
    private string[] wallJumpSfx = {"wallClimb1", "wallClimb2", "wallClimb3"};
    
    [Header("Wall climb")]
    [SerializeField] private float wallJumpForce;
    [SerializeField] private float wallJumpKickback;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask climbable;
    
    void Start()
    {
        SoundManager.instance.StopMusic();
        SoundManager.instance.PlayMusic("large");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        if (input != 0 && Mathf.Abs(rb.velocity.x) < maxSpeed)
            rb.velocity += new Vector2(input * accelereation * Time.deltaTime, 0);
        else
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, 0f, deceleration * Time.deltaTime), rb.velocity.y);

        // I know this is a mess but it's a game jam game, this was the best i could come up with to play sounds on landing
        if (Physics2D.Raycast(transform.position + new Vector3(0.35f, 0f, 0f), Vector2.down, groundCheckDistance,
                groundLayer) || Physics2D.Raycast(transform.position - new Vector3(0.35f, 0f, 0f), Vector2.down,
                groundCheckDistance, groundLayer))
        {
            if (!isGrounded)
            {
                // Idk it gets louder if you move faster on the x axis even though it's a y axis sound
                /*
                var volume = Mathf.Clamp(Mathf.Abs(rb.velocity.y) / 10, 0.1f, 1f);
                SoundManager.instance.SetSFXVolume(volume);
                */
                var random = Random.Range(0, sfx.Length);
                SoundManager.instance.PlayPanSFX(sfx[random]);
                isGrounded = true;
            }
            
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
                rb.velocity += new Vector2(0f, jumpForce);
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, climbable))
            {
                SoundManager.instance.PlayPanSFX(wallJumpSfx[Random.Range(0, wallJumpSfx.Length)]);
                rb.velocity = new Vector2(-wallJumpKickback, wallJumpForce);
            }

            if (Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, climbable))
            {
                SoundManager.instance.PlayPanSFX(wallJumpSfx[Random.Range(0, wallJumpSfx.Length)]);
                rb.velocity = new Vector2(wallJumpKickback, wallJumpForce);
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(0.35f, 0f, 0f), transform.position + new Vector3(0.35f, 0f, 0f) + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(transform.position - new Vector3(0.35f, 0f, 0f), transform.position - new Vector3(0.35f, 0f, 0f) + Vector3.down * groundCheckDistance);  
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * wallCheckDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * wallCheckDistance);
    }

    public int getSize()
    {
        return 3;
    }
}
