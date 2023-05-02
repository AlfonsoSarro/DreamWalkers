using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public float range;
    public float shootingSpeed;

    private float timer;
    private GameObject player;

    [SerializeField] private AudioSource shootAudio;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance < range)
        {
            if (timer > shootingSpeed)
            {
                shootAudio.Play();
                timer = 0;
                Instantiate(bullet, bulletPos.position, Quaternion.identity);
            }
        }
        
    }
}
