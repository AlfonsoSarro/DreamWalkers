using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction jumpAction; //Store our controls
    private InputAction moveAction; //Store our controls
    private InputAction crouchAction; //Store our controls
    private InputAction quitAction; //Store our controls

    public float speed;
    public float jumpHeight;

    private Animator animator;
    //Vector that will store the position of the respawn
    private static Vector3 respawnPoint = new Vector3(-1, -5.5f, 0);
    new Rigidbody2D rigidbody;
    new CapsuleCollider2D collider;
    
    bool grounded = false;
    bool canDoubleJump = false;
    bool facingLeft = false;
    bool crouching = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions["Jump"];
        moveAction = playerInput.actions["Move"];
        crouchAction = playerInput.actions["Crouch"];
        quitAction = playerInput.actions["Quit"];
        transform.position = respawnPoint;
    }
    
    private void OnDisable()
    {
        jumpAction.performed -= Jump;
        jumpAction.canceled -= StopJump;
        crouchAction.performed -= Crouch;
        crouchAction.canceled -= StopCrouch;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        jumpAction.performed += Jump;
        jumpAction.canceled += StopJump;
        crouchAction.performed += Crouch;
        crouchAction.canceled += StopCrouch;
        quitAction.performed += Quit;
        respawnPoint = transform.position;
        print("Set respawn to " + respawnPoint);
    }

    private void Quit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
    void Jump(InputAction.CallbackContext context)
    {
        
        if (grounded || canDoubleJump)
        {
            animator.SetBool("Grounded", false);
            animator.SetBool("Falling", false);
            Vector2 velocity = rigidbody.velocity;
            velocity.y = jumpHeight;
            if (!grounded)
            {
                animator.SetBool("DoubleJump", true);
                canDoubleJump = false;
            }
            else
            {
                animator.SetBool("Jumping", true);
                grounded = false;
            }
            rigidbody.velocity = velocity;
        }
    }

    void StopJump(InputAction.CallbackContext context)
    {
        if(rigidbody.velocity.y > 0f)
        {
            Vector2 velocity = rigidbody.velocity;
            velocity.y = rigidbody.velocity.y * 0.5f;
            rigidbody.velocity = velocity;
        }
    }

    void Crouch(InputAction.CallbackContext context)
    {
        crouching = true;
        rigidbody.velocity = new Vector2(0, 0);
        Vector2 colliderSize = collider.size;
        Vector2 colliderOffset = collider.offset;
        colliderSize.y = collider.size.y / 2;
        colliderOffset.y = collider.offset.y - (collider.size.y / 4);
        collider.size = colliderSize;
        collider.offset = colliderOffset;
        animator.SetBool("Crouching", true);
    }

    void StopCrouch(InputAction.CallbackContext context)
    {
        Vector2 colliderSize = collider.size;
        Vector2 colliderOffset = collider.offset;
        colliderSize.y = collider.size.y * 2;
        colliderOffset.y = 0.73f;
        collider.size = colliderSize;
        collider.offset = colliderOffset;
        animator.SetBool("Crouching", false);
        crouching = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
        canDoubleJump = true;
        animator.SetBool("Falling", false);
        animator.SetBool("Grounded", true);
        animator.SetBool("DoubleJump", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Checkpoint":
                respawnPoint = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
                break;
        }
    }

    void Update()
    {
        if(!crouching)
        {
            Vector2 charVelocity = rigidbody.velocity;
            charVelocity.x = moveAction.ReadValue<float>() * speed;
            rigidbody.velocity = charVelocity;
            if (charVelocity.x < 0 && !facingLeft)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                facingLeft = true;
            }
            if (charVelocity.x > 0 && facingLeft)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                facingLeft = false;
            }
            animator.SetFloat("Velocity", Mathf.Abs(charVelocity.x));
            if (charVelocity.y < -0.1f)
            {
                grounded = false;
                animator.SetBool("Falling", true);
                animator.SetBool("Grounded", false);
                animator.SetBool("Jumping", false);
            }
        }
        
        
    }
}
