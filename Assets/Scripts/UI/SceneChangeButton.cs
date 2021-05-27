using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneChangeButton : MonoBehaviour
{
    [Tooltip("If true, button goes to the next scene, if false, will go to the previous")]
    public bool Next;
    private Button button;
    SceneChangeManager manager {
        get => SceneChangeManager.Instance;
    }
    void Start()
    {
        if (manager == null)
            return;
        button = GetComponent<Button>();
        if (Next) {
            button.onClick.AddListener(manager.GoToNextScene);
        }
        else {
            button.onClick.AddListener(manager.GoToPrevScene);
        }
    }
}
