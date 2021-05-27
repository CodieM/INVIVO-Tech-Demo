using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{    
    public List<string> ContentSceneNames;
    public string mainMenuSceneName;
    int currentSceneIndex = -1;
    public static SceneChangeManager Instance { get; private set; }
    void Awake() {
        if (Instance != null) {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public void ChangeScene(int index) {
        if (index >= 0 && index < ContentSceneNames.Count && index != currentSceneIndex) {
            SceneManager.LoadScene(ContentSceneNames[index]);
            currentSceneIndex = index;
        }
    }

    public void GoToNextScene() {
        ChangeScene((currentSceneIndex + 1) % ContentSceneNames.Count);
    }
    public void GoToPrevScene() {
        ChangeScene((currentSceneIndex - 1) % ContentSceneNames.Count);
    }
    public void GoToMenu() {
        SceneManager.LoadScene(mainMenuSceneName);
        currentSceneIndex = -1;
    }
}
