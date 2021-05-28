using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour
{
    public Transform Pivot;
    public Camera mainCam;
    float degreesPerSecond = 0f;
    public float friction = 0.125f;
    private Vector3 lastFrameHit;
    private bool hitLastFrame = false;
    private void Start() {
        mainCam = Camera.main;
    }
    private void OnEnable() {
        degreesPerSecond = -45f;
    }

    void Update() {
        float deltaAngle = degreesPerSecond * Time.deltaTime;
        if (Input.GetMouseButton(0)) {
            var ray = mainCam.ScreenPointToRay(Input.mousePosition);
            var windmillPlane = new Plane(Vector3.forward, Pivot.position);
            if (windmillPlane.Raycast(ray, out float hitDist)) {
                var cur = ray.GetPoint(hitDist);
                if (hitLastFrame) {
                    deltaAngle = Quaternion.FromToRotation(Pivot.position - lastFrameHit,Pivot.position -  cur).eulerAngles.z;
                    if (deltaAngle > 180f) {
                        deltaAngle -= 360f;
                    }
                    degreesPerSecond = deltaAngle / Time.deltaTime;
                }
                hitLastFrame = true;
                lastFrameHit = cur;
            }
        }
        else {
            hitLastFrame = false;
        }
        Spin(deltaAngle);
        degreesPerSecond = Mathf.MoveTowards(degreesPerSecond, 0, friction);
    }

    private void Spin(float delta)
    {
        Pivot.Rotate(Vector3.forward * delta);
    }
}
