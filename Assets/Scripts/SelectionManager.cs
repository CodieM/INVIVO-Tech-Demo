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
        // Highlight selection
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0)) {
            var found = GetTarget();
            if (found != null && found != selectedObject&& found != hoveredObject) {
                if (hoveredObject != null)
                    hoveredObject.RemoveHighlighting();
                hoveredObject = found;
                hoveredObject.Highlight();
            }
            
        }
        // Select hovered object
        if (Input.GetMouseButtonUp(0)) {
            var found = GetTarget();
            if (found != null) {
                hoveredObject = null;
                if (selectedObject != null)
                    selectedObject.Deselect();
                selectedObject = found;
                selectedObject.Select();
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
