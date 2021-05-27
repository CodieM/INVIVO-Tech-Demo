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
    int currentTransition = 0;
    public static SceneChangeManager Instance { get; private set; }
    SceneTransition[] transitions; 
    void Awake() {
        if (Instance != null) {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        transitions = GetComponentsInChildren<SceneTransition>();
        DontDestroyOnLoad(this.gameObject);
    }
    public void ChangeScene(int index) {
        if (index >= 0 && index < ContentSceneNames.Count && index != currentSceneIndex) {
            StartCoroutine(TransitionToScene(ContentSceneNames[index]));
            currentSceneIndex = index;
        }
    }

    public void GoToNextScene() {
        ChangeScene((currentSceneIndex + 1) % ContentSceneNames.Count);
        
    }
    public void GoToPrevScene() {
        ChangeScene((currentSceneIndex + ContentSceneNames.Count - 1) % ContentSceneNames.Count);
    }
    public void GoToMenu() {
        currentSceneIndex = -1;
        StartCoroutine(TransitionToScene(mainMenuSceneName));
    }

    IEnumerator TransitionToScene(string sceneName) {
        yield return new WaitForEndOfFrame();
        var screenCap = ScreenCapture.CaptureScreenshotAsTexture();
        SceneManager.LoadScene(sceneName);
        yield return transitions[currentTransition].Transition(screenCap);
        currentTransition = (currentTransition + 1) % transitions.Length;
    }
}
