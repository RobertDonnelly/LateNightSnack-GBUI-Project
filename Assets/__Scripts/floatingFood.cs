using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingFood : MonoBehaviour
{
    public float hoverpower = 20f;

     void OnTriggerStay(Collider other)
    {
        other.attachedRigidbody.AddForce(Vector3.up * hoverpower , ForceMode.Acceleration);
    }
}
