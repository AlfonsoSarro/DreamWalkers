using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    private Vector2 destPos;
    private Vector3 velocity = Vector3.zero;
    private static Vector3 cameraPos = new Vector3(0, 0, -10);


    private void Awake()
    {
        transform.position = cameraPos;
        destPos.x = cameraPos.x;
        destPos.y = cameraPos.y;
    }
    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(destPos.x, destPos.y, transform.position.z), ref velocity, speed);
    }

    public void MoveToNewRoom(Vector2 newRoom)
    {
        destPos.x = newRoom.x;
        destPos.y = newRoom.y;
        cameraPos = new Vector3(newRoom.x, newRoom.y, transform.position.z);
        
    }
}
