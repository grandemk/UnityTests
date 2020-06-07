using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    
    [HideInInspector] public Rigidbody rigidBody;
    public float damageRadius = 1f;

    // Called when you first add the component to a GameObject
    void Reset()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
}
