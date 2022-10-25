using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction jumpAction; //Store our controls
    private InputAction moveAction; //Store our controls
    public float speed;
    public float jumpHeight;
    new Rigidbody2D rigidbody;
    
    bool grounded = false;
    bool canDoubleJump = false;
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
            Vector2 velocity = rigidbody.velocity;
            velocity.y = jumpHeight;
            if(!grounded)
            {
                canDoubleJump = false;
            }
            else
            {
                grounded = false;
            }
            rigidbody.velocity = velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
        canDoubleJump = true;
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
