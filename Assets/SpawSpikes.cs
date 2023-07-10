using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawSpikes : MonoBehaviour
{

    public GameObject[] spikes;
    private BoxCollider2D ownCollider;

    // Start is called before the first frame update
    void Start()
    {
        ownCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            spikes[0].active = true;
            spikes[1].active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ownCollider.enabled = false;

    }
}
