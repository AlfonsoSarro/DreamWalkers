using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float destroyDelay;
    public bool fallUp;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            
            if(fallUp)
            {
                rb.velocity = new Vector2(0, 15);
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            Destroy(gameObject, destroyDelay);
        }
    }
}
