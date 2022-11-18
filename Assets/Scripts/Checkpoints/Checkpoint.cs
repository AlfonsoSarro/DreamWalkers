using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector2 previousRoom;
    public Vector2 nextRoom;
    public CameraController camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.transform.position.x < transform.position.x)
            {
                camera.MoveToNewRoom(nextRoom);
            }
            else
            {
                camera.MoveToNewRoom(previousRoom);
            }
        }
    }
}
