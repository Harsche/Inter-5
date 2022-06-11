using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeInOut : MonoBehaviour{
    [SerializeField] Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private Canvas myCanvas;

    private void Awake(){
        myCanvas = GetComponent<Canvas>();
    }

    private void Start(){
        myCanvas.enabled = true;
        fadeImage.DOFade(0f, fadeDuration);
    }
}