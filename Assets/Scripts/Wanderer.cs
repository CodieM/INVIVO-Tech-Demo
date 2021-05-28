using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Wanderer : MonoBehaviour
{
    public float WanderRadius = 10f;
    NavMeshAgent agent;
    Vector3 startPos;
    Vector3 targetPos;
    Vector3 lastFramePos;

    private void Start() {
        startPos = transform.position;
    }
    void OnEnable() {
        agent = GetComponentInParent<NavMeshAgent>();
        lastFramePos = targetPos = transform.position;
    }
    private void OnDisable() {
        targetPos = transform.position;
        agent.SetDestination(targetPos);
    }
    private Vector3 GetNewTargetPos()
    {
        var randPos = Random.insideUnitCircle;
        return startPos + new Vector3(randPos.x, 0, randPos.y) * Random.Range(0f, WanderRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.25f || lastFramePos == transform.position) {
            var potentialPath = GetNewTargetPos();
            if (agent.SetDestination(potentialPath))
                targetPos = potentialPath;

        }
        lastFramePos = transform.position;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Handles.DrawWireDisc(targetPos, Vector3.up, 1f);
    }
#endif
}
