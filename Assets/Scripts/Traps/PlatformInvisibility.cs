using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInvisibility : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 4f;
    public SpriteRenderer platform;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        platform.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        if(currentTime > 1)
        {
            platform.enabled = false;
        }
        else
        {
            platform.enabled = true;
        }
        if(currentTime <= 0)
        {
            currentTime = startingTime;
        }
    }
}
