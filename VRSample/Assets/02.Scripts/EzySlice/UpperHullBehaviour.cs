using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperHullBehaviour : MonoBehaviour
{
    public Vector3 velocity;
    private float _decel = 1.0f;

    private void Start()
    {
        transform.position += velocity.normalized * 0.2f;
    }

    private void FixedUpdate()
    {
        transform.position += velocity * Time.fixedDeltaTime;
        transform.Rotate(Vector3.down + Vector3.forward, 20.0f * Time.fixedDeltaTime, Space.Self);
        velocity -= velocity.normalized * _decel * Time.fixedDeltaTime;

        if (velocity.magnitude <= 0.1f)
            Destroy(this.gameObject);
    }
}