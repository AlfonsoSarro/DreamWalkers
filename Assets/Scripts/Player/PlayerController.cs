using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction jumpAction; //Store our controls
    private InputAction moveAction; //Store our controls
    public float speed;
    public float jumpHeight;
    public Animator animator;
    new Rigidbody2D rigidbody;
    
    bool grounded = false;
    bool canDoubleJump = false;
    bool facingLeft = false;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions["Jump"];
        moveAction = playerInput.actions["Move"];
    }
    
    private void OnDisable()
    {
        jumpAction.performed -= Jump;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        jumpAction.performed += Jump;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
        canDoubleJump = true;
        animator.SetBool("Falling", false);
        animator.SetBool("Grounded", true);
        animator.SetBool("DoubleJump", false);
    }
    /*
    void Move(InputAction.CallbackContext context)
    {
        Vector2 velocity = rigidbody.velocity;
        velocity.x = moveAction.ReadValue<float>() * speed;
        rigidbody.velocity = velocity;
    }
    */
    void Update()
    {
        Vector2 velocity = rigidbody.velocity;
        velocity.x = moveAction.ReadValue<float>() * speed;
        rigidbody.velocity = velocity;
        if(velocity.x < 0 && !facingLeft)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            facingLeft = true;
        }
        if (velocity.x > 0 && facingLeft)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            facingLeft = false;
        }
        animator.SetFloat("Velocity", Mathf.Abs(velocity.x));
        if(velocity.y < 0)
        {
            grounded = false;
            animator.SetBool("Falling", true);
            animator.SetBool("Grounded", false);
            animator.SetBool("Jumping", false);
        }
    
        /*
        //Read movement value
        float movementInput = playerControls.Grounded.Move.ReadValue<float>();
        print("[PlayerController] Movement value read is " + movementInput);
        Vector2 velocity = rigidbody.velocity;
        velocity.x = movementInput * speed;
        
        print("[PlayerController] Rigidbody velocity in X is " + velocity.x);
        float jumpInput = playerControls.Grounded.Jump.ReadValue<float>();
        if(jumpInput != 0 && !onAir)
        {
            onAir = true;
            velocity.y = 5f;
        }
        
        rigidbody.velocity = velocity;
        */
    }

    
}
