using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Transform cameraTransform;

    void Start() {
        cameraTransform = Camera.main.transform;
    }
    void Update() {
        transform.LookAt(transform.position - cameraTransform.position, Vector3.up);
    }
}
