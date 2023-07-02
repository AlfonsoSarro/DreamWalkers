using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObject : MonoBehaviour
{
    public SpriteRenderer trapSpriteRenderer;
    public float fadeInDuration = 1f;

    private float currentAlpha = 0f;
    private float targetAlpha = 1f;
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
            StartFadeIn();
            activated = true;
        }
    }

    private void Update()
    {
        if (activated && fadeTimer < fadeInDuration)
        {
            fadeTimer += Time.deltaTime;
            currentAlpha = Mathf.Lerp(0f, targetAlpha, fadeTimer / fadeInDuration);
            trapSpriteRenderer.color = new Color(trapSpriteRenderer.color.r, trapSpriteRenderer.color.g, trapSpriteRenderer.color.b, currentAlpha);
        }
    }

    private void StartFadeIn()
    {
        fadeTimer = 0f;
        currentAlpha = 0f;
        trapSpriteRenderer.enabled = true;
    }
}
