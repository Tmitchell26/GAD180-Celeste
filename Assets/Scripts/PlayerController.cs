using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;
    public int health;
    public int playerDamage;

    public Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpValue;

    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    public float dashDistance = 15f;
    bool isDashing;
    float doubleTapTime;
    KeyCode lastKeyCode;

    public Animator animator;

    public float gravity = 3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(moveInput));
       
        // Dashing Left
        if (facingRight == false && Input.GetButtonDown("Dash"))
        {
            StartCoroutine(Dash(-1f));
        }

        // Dashing Right 
        if (facingRight == true && Input.GetButtonDown("Dash"))
        {  
                StartCoroutine(Dash(1f));
        }

        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        if (isTouchingFront == true && isGrounded == false && moveInput != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (!isDashing)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        
        if (facingRight == false && moveInput > 0)
        {
            Flip(); 
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();  
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    IEnumerator Dash(float direction)
    {
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        rb.gravityScale = gravity;
    }

    // function to simulate the player taking damage
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            PlayerManager.instance.KillPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyStats enemy = collision.collider.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= 0.9f)
                {
                    Vector2 velocity = rb.velocity;
                    velocity.y = jumpForce;
                    rb.velocity = velocity;
                    enemy.TakeDamage(playerDamage);
                }
                else
                {
                    TakeDamage(enemy.gameObject.GetComponent<EnemyStats>().enemyDamage);
                }
            }
        }
    }
}
