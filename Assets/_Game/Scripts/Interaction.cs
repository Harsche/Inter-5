using System;
using UnityEngine;
using DG.Tweening;

public class Interaction : MonoBehaviour, IInteractable{
    private Outline outline;

    private void Awake(){
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void Update(){
        if(this != PlayerInteraction.interactable)
            ToggleGlow(false);
    }

    public void ToggleGlow(bool value){
        outline.enabled = value;
    }

    public void Interact(){
        Debug.Log("Interaction done!");
        ToggleGlow(true);
        Tween tween = transform.DOScale(Vector3.zero, 0.5f);
        tween.SetLink(gameObject);
        tween.OnComplete(() => gameObject.SetActive(false));
    }
}