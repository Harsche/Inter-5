using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class InteractionGameEvent : MonoBehaviour, IInteractable{
    [SerializeField] private UnityEvent onInteract;

    private Outline outline;

    private void Awake(){
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void Update(){
        if (this != PlayerInteraction.interactable)
            ToggleGlow(false);
    }

    public void ToggleGlow(bool value){
        outline.enabled = value;
    }

    public void Interact(){
        ToggleGlow(true);
        Tween tween = transform.DOScale(Vector3.zero, 0.5f);
        tween.SetLink(gameObject);
        tween.OnComplete(() => gameObject.SetActive(false));
        onInteract?.Invoke();
    }
}