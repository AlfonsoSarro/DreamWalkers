using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    public GameObject enemy;
    public int jump;

    private BoxCollider2D ownCollider;

    private void Start()
    {
        ownCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ownCollider.enabled = false;
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, jump);
            enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
