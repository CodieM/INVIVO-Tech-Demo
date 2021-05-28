using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class SceneTransition : MonoBehaviour
{
    Animator transition;
    public RawImage[] backgrounds;
    public float transitonDuration = 1f;
    public void Start() {
        transition = GetComponent<Animator>();
    }
    public IEnumerator Transition(Texture2D texture) {
        foreach(var bg in backgrounds) {
            bg.texture = texture;
        }
        
        transition.speed = transitonDuration;
        transition.SetTrigger("Play");
        yield return new WaitForSeconds(transitonDuration);
    }
}
