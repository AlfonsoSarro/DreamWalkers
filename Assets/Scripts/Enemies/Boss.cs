using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private SpriteRenderer sprite;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;
    private BoxCollider2D boxCollider;

    public GameObject winMenu;

    private bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
        {
            Vector2 direction = player.GetComponent<Renderer>().bounds.center - transform.position;
            if (direction.x < 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
            transform.position = new Vector2(Mathf.MoveTowards(this.transform.position.x, player.GetComponent<Renderer>().bounds.center.x, speed * Time.deltaTime), transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "DeathTrigger")
        {
            capsuleCollider.enabled = false;
            animator.SetBool("Die", true);
            boxCollider.size = new Vector2(0.01f, 0.01f);
            boxCollider.offset = new Vector2(0f, -0.3f);
            dead = true;
            winMenu.SetActive(true);
        }
    }
}
