using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    Transform mainCam;
    public float orbitDist = 40f;
    public float orbitSpeed = 20f;
    public float timeToMaxSpeed = 3f;
    float startTime;
    void Start()
    {
        mainCam = Camera.main.transform;
    }
    private void OnEnable() {
        startTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        mainCam.RotateAround(transform.position, Vector3.up, Mathf.SmoothStep(0, orbitSpeed*Time.deltaTime, timeToMaxSpeed));
    }
}
