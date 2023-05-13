using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public GameObject objectToMove;
    public int speed;
    public Vector2 newPos;

    private Transform transform;
    private bool move = false;


    private void Start()
    {
        transform = objectToMove.GetComponent<Transform>();
    }

    private void Update()
    {
        if(move)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            move = true;
        }
    }
}
