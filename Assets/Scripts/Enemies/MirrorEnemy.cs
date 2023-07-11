using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MirrorEnemy : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private Rigidbody2D rigidbody;
    private Animator animator;

    public float speed;

    bool facingLeft = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyVelocity = rigidbody.velocity;
        enemyVelocity.x = moveAction.ReadValue<float>() * -speed;
        rigidbody.velocity = enemyVelocity;
        if (enemyVelocity.x < 0 && !facingLeft)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            facingLeft = true;
        }
        if (enemyVelocity.x > 0 && facingLeft)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            facingLeft = false;
        }
        animator.SetFloat("Velocity", Mathf.Abs(enemyVelocity.x));
    }
}
