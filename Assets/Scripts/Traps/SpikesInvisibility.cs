using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesInvisibility : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 4f;
    public SpriteRenderer spikes;
    public BoxCollider2D spikesBox;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        spikes.enabled = false;
        spikesBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        if (currentTime > 1)
        {
            spikes.enabled = false;
            spikesBox.enabled = false;
        }
        else
        {
            spikes.enabled = true;
            spikesBox.enabled = true;
        }
        if (currentTime <= 0)
        {
            currentTime = startingTime;
        }
    }
}
