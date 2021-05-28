using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public Transform GravitySource;
    public float Velocity = 10f;
    public float IdealDistance = 30f;
    private void Update() {
        transform.Translate(Vector3.forward * Velocity * Time.deltaTime);
        Plane perpPlane = new Plane(transform.position - GravitySource.position, transform.position);
        var newForward = perpPlane.ClosestPointOnPlane(transform.position + transform.forward) - transform.position;
        transform.forward = Vector3.Lerp(transform.forward, newForward, Mathf.Clamp01(Vector3.Distance(GravitySource.position, transform.position) / IdealDistance));
    }
}
