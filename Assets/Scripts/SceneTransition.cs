using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class SceneTransition : MonoBehaviour
{
    Animator transition;
    public RawImage background;
    public float transitonDuration = 1f;
    public void Start() {
        transition = GetComponent<Animator>();
    }
    public IEnumerator Transition(Texture2D texture) {
        background.texture = texture;
        transition.speed = transitonDuration;
        transition.SetTrigger("Play");
        yield return new WaitForSeconds(transitonDuration);
    }
}
