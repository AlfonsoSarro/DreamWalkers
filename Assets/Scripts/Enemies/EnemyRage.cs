using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRage : MonoBehaviour
{
    public GameObject enemy;

    private Rigidbody2D rb;
    private EnemyPatrol patrolScript;
    private EnemyRun enemyRunScript;
    // Start is called before the first frame update
    void Start()
    {
        patrolScript = enemy.GetComponent<EnemyPatrol>();
        enemyRunScript = enemy.GetComponent<EnemyRun>();
        rb = enemy.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            patrolScript.enabled = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 50;
            enemyRunScript.enabled = true;
            
        }
    }
}
