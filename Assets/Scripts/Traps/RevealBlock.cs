using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealBlock : MonoBehaviour
{
    public BoxCollider2D collider;

    private SpriteRenderer spriteRenderer;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(player.GetComponent<Rigidbody2D>().velocity.y > 0f)
        {
            spriteRenderer.enabled = true;
            collider.enabled = true;
        }
        
    }
}
