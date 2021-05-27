using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneSelectionButton : MonoBehaviour
{
    public int TargetSceneIndex;
    private Button button;
    SceneChangeManager manager {
        get => SceneChangeManager.Instance;
    }
    void Start()
    {
        if (manager == null)
            return;
        button = GetComponent<Button>();
        button.onClick.AddListener(
            delegate {
                manager.ChangeScene(TargetSceneIndex);
            }
        );
    }
}
