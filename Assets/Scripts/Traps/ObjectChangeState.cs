using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChangeState : MonoBehaviour
{
    public GameObject objectToEnable;
    public bool setActive;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            objectToEnable.SetActive(setActive);
        }
    }
}
