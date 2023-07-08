using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesFalling : MonoBehaviour
{
    private BoxCollider2D ownCollider;
    public Rigidbody2D spike;
    // Start is called before the first frame update
    void Start()
    {
        ownCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            spike.isKinematic = false;
        }
    }
}
