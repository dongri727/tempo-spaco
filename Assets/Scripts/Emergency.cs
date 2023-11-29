using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emergency : MonoBehaviour
{
    public Rigidbody rb;

    public void StopRotation()
    {
        rb.angularVelocity = Vector3.zero;
    }
}