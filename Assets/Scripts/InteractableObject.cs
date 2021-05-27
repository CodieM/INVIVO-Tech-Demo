using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InteractableObject : MonoBehaviour
{
    public string ObjectTitle;
    public TMPro.TextMeshProUGUI ObjectLabel;
    public List<GameObject> DisableWhenInactive;
    bool active = false;
    void Start()
    {
        ObjectLabel.text = ObjectTitle;
        if (!active) {
            foreach (var obj in DisableWhenInactive) {
                obj.SetActive(false);
            }
        }
    }
    public void Highlight() {

    }
    public void Select() {
        SetDisableListVisibility(true);
    }


    public void RemoveHighlighting() {

    }
    public void Deselect() {
        SetDisableListVisibility(false);
    }
    private void SetDisableListVisibility(bool vis)
    {
        foreach (var obj in DisableWhenInactive)
        {
            obj.SetActive(vis);
        }
    }
}
