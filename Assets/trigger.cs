using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class trigger : MonoBehaviour
{
    public UnityEvent toTrigger;
    // Start is called before the first frame update
    void Start()
    {
        toTrigger?.Invoke();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
