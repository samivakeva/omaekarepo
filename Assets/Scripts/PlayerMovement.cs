using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveInputX;
    public float moveInputY;
    public float playerSpeed;
    public float climbSpeed;
    public Rigidbody2D rb;
    public bool isFacingRight;
    public SpriteRenderer sprite;
    public bool isGrounded;
    public float jumpForce;
    public bool canClimb;

    public

    void Start()
    { }

    float ControlY()
    {
        if (canClimb)
        {
            return moveInputY * climbSpeed;
        }
        else
        {
            return rb.linearVelocity.y;
        }
    }

    void PlayerMove()
    {
        moveInputX = Input.GetAxis("Horizontal");
        moveInputY = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector2(moveInputX * playerSpeed, ControlY());
    }

    void FlipSprite()
    {
        if (moveInputX < 0 && isFacingRight)
        {
            isFacingRight = false;
            sprite.flipX = false;
        }
        if (moveInputX > 0 && !isFacingRight)
        {
            isFacingRight = true;
            sprite.flipX = true;
        }
    }

    void JumpControl()
    {
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jumping");
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    //Enable jumping
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

    //Enable climbing
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LADDER"))
        {
            climbSpeed = 6f;
            rb.gravityScale = 0;
            canClimb = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LADDER"))
        {
            climbSpeed = 0;
            rb.gravityScale = 20;
            canClimb = false;
        }
    }

    void Update()
    {
        JumpControl();
        PlayerMove();
        FlipSprite();
    }
}

