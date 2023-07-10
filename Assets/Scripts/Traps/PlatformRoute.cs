using UnityEngine;

public class PlatformRoute : MonoBehaviour
{
    public Transform objectToMove;
    public float speed;
    private int[] targetXPositions = { 140, 140, 127, 127, 144 };
    private float[] targetYPositions = { 13.5f, 11.5f, 11.5f, 8.6f, 8.6f };
    private int currentTargetIndex = 0;
    private bool isMoving = false;
    private Vector2 initialPosition;
    private Vector2 targetPosition;
    private float t = 0f;

    private void Start()
    {
        initialPosition = objectToMove.position;
        isMoving = false;
    }

    private void Update()
    {
        if (isMoving)
        {
            t += GetSpeed() * Time.deltaTime;
            objectToMove.position = Vector2.Lerp(initialPosition, targetPosition, t);

            if (objectToMove.position == (Vector3)targetPosition)
            {
                isMoving = false;
                currentTargetIndex++;
                t = 0f;

                if (currentTargetIndex < targetXPositions.Length)
                {
                    SetTargetPosition(targetXPositions[currentTargetIndex], targetYPositions[currentTargetIndex]);
                }
                else
                {
                    Debug.Log("Se han alcanzado todas las posiciones objetivo");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isMoving)
        {
            if (currentTargetIndex < targetXPositions.Length)
            {
                SetTargetPosition(targetXPositions[currentTargetIndex], targetYPositions[currentTargetIndex]);
            }
        }
    }

    private void SetTargetPosition(int x, float y)
    {
        initialPosition = objectToMove.position;
        targetPosition = new Vector2(x, y);
        isMoving = true;

        Debug.Log("Moviendo a la posición: (" + x + ", " + y + ")");
    }

    private float GetSpeed()
    {
        float currentSpeed = speed;

        // Verificar si el movimiento es en el eje vertical
        if (Mathf.Approximately(initialPosition.x, targetPosition.x))
        {
            currentSpeed *= 3f; // Duplicar la velocidad en el eje vertical
        }

        return currentSpeed;
    }
}
