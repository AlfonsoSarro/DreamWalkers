using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRun : MonoBehaviour
{
    public int speed;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed, 0);
    }
}
