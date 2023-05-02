using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private Transform enemyTransform;

    [SerializeField] private AudioSource chaseAudio;

    // Start is called before the first frame update
    void Start()
    {
        enemyTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.transform.position - transform.position;

        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if((IsFacingRight() && PlayerIsFacingRight()) || (!IsFacingRight() && !PlayerIsFacingRight()))
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            chaseAudio.loop = true;
            chaseAudio.Play();
        }
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        
    }

    private bool IsFacingRight()
    {
        Debug.Log(transform.rotation.z);
        return enemyTransform.rotation.z >= -0.7f && enemyTransform.rotation.z <= 0.7f;
    }

    private bool PlayerIsFacingRight()
    {
        return player.transform.localScale.x > 0f;
    }
}
