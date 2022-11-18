using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingTrap : MonoBehaviour
{
    public Transform trap;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        trap.position = new Vector3(trap.position.x, trap.position.y + 3, trap.position.z);
        Destroy(GetComponent<BoxCollider2D>());
    }
}
