using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private PlayerInput playerInput;
    private InputAction jumpAction; //Store our controls
    private InputAction moveAction; //Store our controls
    private InputAction crouchAction; //Store our controls
    private InputAction quitAction; //Store our controls

    public float speed;
    public float jumpHeight;
    public LayerMask groundLayerMask;
    public float coyoteTime = 0.2f;
    public LifeSO lifes;
    public Text lifesText;

    private Animator animator;
    //Vector that will store the position of the respawn
    private static Vector3 respawnPoint = new Vector3(-1, -5.5f, 0);
    new Rigidbody2D rigidbody;
    new CapsuleCollider2D collider;
    private float coyoteCounter;
    
    bool canDoubleJump = false;
    bool facingLeft = false;
    bool crouching = false;

    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource doubleJumpAudio;

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
        lifesText.text = "x " + lifes.Value.ToString();
    }

    private void Quit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
    void Jump(InputAction.CallbackContext context)
    {
        if(!crouching)
        {
            if (coyoteCounter > 0f || canDoubleJump)
            {
                animator.SetBool("Grounded", false);
                animator.SetBool("Falling", false);
                Vector2 velocity = rigidbody.velocity;
                velocity.y = jumpHeight;
                if (!(coyoteCounter > 0f))
                {
                    doubleJumpAudio.Play();
                    animator.SetBool("DoubleJump", true);
                    canDoubleJump = false;
                }
                else
                {
                    animator.SetBool("Jumping", true);
                    jumpAudio.Play();
                }
                rigidbody.velocity = velocity;
            }
        }
        
        
    }

    void StopJump(InputAction.CallbackContext context)
    {
        if(rigidbody.velocity.y > 0f)
        {
            Vector2 velocity = rigidbody.velocity;
            velocity.y = rigidbody.velocity.y * 0.5f;
            rigidbody.velocity = velocity;
            coyoteCounter = 0f;
        }
    }

    void Crouch(InputAction.CallbackContext context)
    {
        if (IsGrounded()) {
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
    }

    void StopCrouch(InputAction.CallbackContext context)
    {
        if (IsGrounded())
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Checkpoint":
                respawnPoint = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
                break;
            case "DeathTrigger":
                lifes.Value--;
                lifesText.text = "x " + lifes.Value.ToString();
                if (lifes.Value == 0)
                {
                    //TODO: Game manager pop up with game over
                    Debug.Log("Game Over");
                    SceneManager.LoadScene("MainMenu");
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                break;
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D rayHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size - new Vector3(0.5f,0f,0f), 0f, Vector2.down, extraHeight, groundLayerMask);
        if(rayHit.collider != null)
        {
            animator.SetBool("Grounded", true);
            animator.SetBool("DoubleJump", false);
            animator.SetBool("Falling", false);
            canDoubleJump = true;
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
        return rayHit.collider != null;
    }

    void Update()
    {
        if(IsGrounded())
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

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
            if (!IsGrounded()) 
            {
                if(charVelocity.y < -0.1f)
                {
                    animator.SetBool("Falling", true);
                    animator.SetBool("Jumping", false);
                }
                else
                {
                    animator.SetBool("Falling", false);
                    animator.SetBool("Jumping", true);
                }
            }
            
        }
        
        
    }
}
