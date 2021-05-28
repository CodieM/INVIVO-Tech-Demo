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
        var screenCap = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);//ScreenCapture.CaptureScreenshotAsTexture();
        screenCap = CaptureScreen(screenCap);
        SceneManager.LoadScene(sceneName);
        yield return transitions[currentTransition].Transition(screenCap);
        currentTransition = (currentTransition + 1) % transitions.Length;
    }

    // Found this after post processing stopped the simple CaptureScreenshotAsTexture from working properly
    // Thanks to mrtenda https://forum.unity.com/threads/how-to-capture-screenshot-with-post-processing.701432/
    Texture2D CaptureScreen(Texture2D ScreenshotTexture) {
        RenderTexture transformedRenderTexture = null;
        RenderTexture renderTexture = RenderTexture.GetTemporary(
            Screen.width,
            Screen.height,
            24,
            RenderTextureFormat.ARGB32,
            RenderTextureReadWrite.Default,
            1);
        try
        {
            ScreenCapture.CaptureScreenshotIntoRenderTexture(renderTexture);
            transformedRenderTexture = RenderTexture.GetTemporary(
                ScreenshotTexture.width,
                ScreenshotTexture.height,
                24,
                RenderTextureFormat.ARGB32,
                RenderTextureReadWrite.Default,
                1);
            Graphics.Blit(
                renderTexture,
                transformedRenderTexture,
                new Vector2(1.0f, -1.0f),
                new Vector2(0.0f, 1.0f));
            RenderTexture.active = transformedRenderTexture;
            ScreenshotTexture.ReadPixels(
                new Rect(0, 0, ScreenshotTexture.width, ScreenshotTexture.height),
                0, 0);
        }
        catch (System.Exception e)
        {
            Debug.Log("Exception: " + e);
        }
        finally
        {
            RenderTexture.active = null;
            RenderTexture.ReleaseTemporary(renderTexture);
            if (transformedRenderTexture != null)
            {
                RenderTexture.ReleaseTemporary(transformedRenderTexture);
            }
        }

        ScreenshotTexture.Apply();
        return ScreenshotTexture;
    }
}
