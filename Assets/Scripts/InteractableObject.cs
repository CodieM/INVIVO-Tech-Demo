using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InteractableObject : MonoBehaviour
{
    public string ObjectTitle;
    public TMPro.TextMeshProUGUI ObjectLabel;
    public List<GameObject> DisableWhenInactive;
    public Material HighlightMat, InactiveMat;
    public Transform CameraViewPosition;
    public float CameraTransitionTime = 3f;
    public List<Renderer> InactiveIgnore = new List<Renderer>();
    Material inactiveMat;
    Renderer[] childRenderers;
    Dictionary<Renderer, Material> activeMatDict = new Dictionary<Renderer, Material>();
    bool active = false;
    Animator animator;
    private static bool overriden = false;
    void Start()
    {
        ObjectLabel.text = ObjectTitle;
        if (!active) {
            foreach (var obj in DisableWhenInactive) {
                obj.SetActive(false);
            }
        }
        animator = GetComponentInChildren<Animator>();
        childRenderers = GetComponentsInChildren<Renderer>();
        foreach (var rend in childRenderers) {
            activeMatDict.Add(rend, rend.material);
            if (!InactiveIgnore.Contains(rend))
                rend.material = InactiveMat;
        }
    }
    public virtual void Highlight() {
        foreach (var rend in childRenderers) {
            if (!InactiveIgnore.Contains(rend))
                rend.material = HighlightMat;
        }
    }
    public virtual void Select() {
        SetDisableListVisibility(true);
        foreach (var rend in childRenderers) {
            rend.material = activeMatDict[rend];
        }
        if (animator != null)
            animator.SetBool("Active", true);
        
        if (CameraViewPosition != null) {
            StartCoroutine(MoveCameraToLocation());
        }
    }

    private IEnumerator MoveCameraToLocation()
    {
        overriden = true;
        float startTime = Time.time;
        float elapsed = 0f;
        Camera mainCam = Camera.main;
        Vector3 startPos = mainCam.transform.position;
        Quaternion startRot = mainCam.transform.rotation;
        CameraViewPosition.LookAt(transform);
        Quaternion finalRot = CameraViewPosition.rotation;
        yield return null;
        overriden = false;
        while ((elapsed = Time.time - startTime) < CameraTransitionTime) {
            if (overriden) {
                break;
            }
            float percentComplete = elapsed / CameraTransitionTime;
            mainCam.transform.position = new Vector3 (
                Mathf.SmoothStep(startPos.x, CameraViewPosition.position.x, percentComplete), 
                Mathf.SmoothStep(startPos.y, CameraViewPosition.position.y, percentComplete), 
                Mathf.SmoothStep(startPos.z, CameraViewPosition.position.z, percentComplete)
            );
            percentComplete = percentComplete * percentComplete * (3f - 2f * percentComplete);
            mainCam.transform.rotation = Quaternion.Lerp(startRot, finalRot, percentComplete);
            yield return null;
        }
    }

    public virtual void RemoveHighlighting() {
        foreach (var rend in childRenderers) {
            rend.material = InactiveMat;
        }
    }
    public virtual void Deselect() {
        SetDisableListVisibility(false);
        foreach (var rend in childRenderers) {
            if (!InactiveIgnore.Contains(rend))
                rend.material = InactiveMat;
        }
        if (animator != null)
            animator.SetBool("Active", false);
    }
    private void SetDisableListVisibility(bool vis)
    {
        foreach (var obj in DisableWhenInactive)
        {
            obj.SetActive(vis);
        }
    }
}
