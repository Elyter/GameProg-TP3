using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpForce = 10f;
    public float dashForce = 20f;
    public float dashCooldown = 3f;
    public float pushForce = 5f;

    private bool isGrounded;
    private bool canDoubleJump;
    private bool isDashing;
    private float dashCooldownTimer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleDashInput();
        UpdateCooldownTimers();
    }

    void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, 0);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        // Flip the character sprite based on the direction
        if (horizontalInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = false;
            }
        }
    }

    void HandleDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && Time.time > dashCooldownTimer)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 dashDirection = new Vector2(horizontalInput, verticalInput).normalized;

            if (dashDirection != Vector2.zero)
            {
                isDashing = true;
                dashCooldownTimer = Time.time + dashCooldown;
                rb.velocity = new Vector2(dashDirection.x * dashForce, rb.velocity.y);
            }
        }
    }

    void UpdateCooldownTimers()
    {
        if (isDashing && rb.velocity.x == 0)
        {
            isDashing = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canDoubleJump = false;
        }

        if (collision.gameObject.CompareTag("Object"))
        {
            // Determine the direction to be pushed (opposite of the object's movement)
            Vector2 pushDirection = -collision.relativeVelocity.normalized;

            // Apply a constant push force
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
