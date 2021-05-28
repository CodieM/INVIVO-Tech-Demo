using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    InteractableObject selectedObject, hoveredObject;
    Camera mainCam;
    void Start() {
        mainCam = Camera.main;
    }
    void Update()
    {
        // Select hovered object
        if (Input.GetMouseButtonUp(0)) {
            var found = GetTarget();
            if (found != null && found != selectedObject) {
                hoveredObject = null;
                if (selectedObject != null)
                    selectedObject.Deselect();
                selectedObject = found;
                selectedObject.Select();
            }
        } 
        // Highlight selection
        else if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0)) {
            var found = GetTarget();
            if (found != null && found != selectedObject && found != hoveredObject) {
                if (hoveredObject != null)
                    hoveredObject.RemoveHighlighting();
                hoveredObject = found;
                hoveredObject.Highlight();
            } 
            // Remove highlighting from previously hovered object
            else if (found != hoveredObject && hoveredObject != null) { 
                hoveredObject.RemoveHighlighting();
                hoveredObject = null;
            }
            
        }

    }
    InteractableObject GetTarget() {
        var ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            var cur = hit.transform;
            while (cur != null) {
                var target = cur.GetComponent<InteractableObject>(); 
                if (target != null) {
                    return target;
                }
                cur = cur.parent;
            }
        }
        return null;
    }
}
