using UnityEngine;

public class MoveToPosition : MonoBehaviour
{
    public Transform objectToMove;
    public float speed;
    public Vector2 targetPosition;

    private bool isMoving = false;

    private void Update()
    {
        if (isMoving)
        {
            float step = speed * Time.deltaTime;
            objectToMove.position = Vector2.MoveTowards(objectToMove.position, targetPosition, step);

            if (objectToMove.position == (Vector3)targetPosition)
            {
                isMoving = false;
                // Aquí puedes agregar código adicional que se ejecute una vez que el objeto llegue a la posición objetivo
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isMoving = true;
        }
    }
}
