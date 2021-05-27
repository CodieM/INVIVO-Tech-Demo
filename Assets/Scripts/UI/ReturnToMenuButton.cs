using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ReturnToMenuButton : MonoBehaviour
{
    private Button button;
    SceneChangeManager manager {
        get => SceneChangeManager.Instance;
    }
    void Start()
    {
        if (manager == null)
            return;
        button = GetComponent<Button>();
        button.onClick.AddListener(manager.GoToMenu);
    }
}