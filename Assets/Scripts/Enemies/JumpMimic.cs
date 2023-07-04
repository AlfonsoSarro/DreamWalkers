using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class JumpMimic : MonoBehaviour
{

    private PlayerInput playerInput;
    private InputAction jumpAction; //Store our controls
    public float speed;
    public float jumpHeight;
    public LayerMask groundLayerMask;
    new BoxCollider2D collider;
    

    Rigidbody2D rb;
    bool canDoubleJump = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions["Jump"];
    }

    private void OnDisable()
    {
        jumpAction.performed -= Jump;
        jumpAction.canceled -= StopJump;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        jumpAction.performed += Jump;
        jumpAction.canceled += StopJump;

    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), transform.localScale.y);
        }
    }

    void Jump(InputAction.CallbackContext context)
    { 
        if (IsGrounded() || canDoubleJump)
        {
            if (!IsGrounded())
            {
                canDoubleJump = false;
            }   
            Vector2 velocity = rb.velocity;
            velocity.y = jumpHeight;
            rb.velocity = velocity;
        }
        


    }

    void StopJump(InputAction.CallbackContext context)
    {
        if (rb.velocity.y > 0f)
        {
            Vector2 velocity = rb.velocity;
            velocity.y = rb.velocity.y * 0.5f;
            rb.velocity = velocity;
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.25f;
        RaycastHit2D rayHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size - new Vector3(0.5f, 0f, 0f), 0f, Vector2.down, extraHeight, groundLayerMask);
        if (rayHit.collider != null)
        {
            canDoubleJump = true;
        }
        return rayHit.collider != null;
    }

}
