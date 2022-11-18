using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTrap : MonoBehaviour
{
    public Rigidbody2D trapBase;
    public Rigidbody2D trapDeco;
    public Rigidbody2D trapDeath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trapBase.isKinematic = false;
        trapDeath.isKinematic = false;
        trapDeco.isKinematic = false;
    }
}
