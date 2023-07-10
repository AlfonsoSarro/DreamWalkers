using UnityEngine;

public class HideObject : MonoBehaviour
{
    public SpriteRenderer trapSpriteRenderer;
    public float fadeOutDuration = 1f;

    private float currentAlpha = 1f;
    private float targetAlpha = 0f;
    private float fadeTimer = 0f;
    private bool activated = false;

    private void Start()
    {
        trapSpriteRenderer.color = new Color(trapSpriteRenderer.color.r, trapSpriteRenderer.color.g, trapSpriteRenderer.color.b, currentAlpha);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activated)
        {
            StartFadeOut();
            activated = true;
        }
    }

    private void Update()
    {
        if (activated && fadeTimer < fadeOutDuration)
        {
            fadeTimer += Time.deltaTime;
            currentAlpha = Mathf.Lerp(1f, targetAlpha, fadeTimer / fadeOutDuration);
            trapSpriteRenderer.color = new Color(trapSpriteRenderer.color.r, trapSpriteRenderer.color.g, trapSpriteRenderer.color.b, currentAlpha);
        }
    }

    private void StartFadeOut()
    {
        fadeTimer = 0f;
        currentAlpha = 1f;
    }
}
