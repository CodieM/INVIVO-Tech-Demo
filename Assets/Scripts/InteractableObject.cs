using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InteractableObject : MonoBehaviour
{
    public string ObjectTitle;
    public TMPro.TextMeshProUGUI ObjectLabel;
    public List<GameObject> DisableWhenInactive;
    public Material HighlightMat, InactiveMat;
    Material inactiveMat;
    Renderer[] childRenderers;
    Dictionary<Renderer, Material> activeMatDict = new Dictionary<Renderer, Material>();
    bool active = false;
    Animator animator;
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
            rend.material = InactiveMat;
        }
    }
    public virtual void Highlight() {
        foreach (var rend in childRenderers) {
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
    }


    public virtual void RemoveHighlighting() {
        foreach (var rend in childRenderers) {
            rend.material = InactiveMat;
        }
    }
    public virtual void Deselect() {
        SetDisableListVisibility(false);
        foreach (var rend in childRenderers) {
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
