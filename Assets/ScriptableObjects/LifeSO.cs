using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LifeSO : ScriptableObject
{
    private int lifes;

    public int Value
    {
        get { return lifes; }
        set { lifes = value; }
    }

}
