using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class InteractionEventTrigger : MonoBehaviour, IInteractable{
    [SerializeField] private bool disableObject;
    [SerializeField] private UnityEvent onInteract;

    private Outline outline;

    private void Awake(){
        outline = GetComponent<Outline>();
        outline.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    private void Update(){
        if (this != PlayerInteraction.Interactable)
            ToggleGlow(false);
    }

    public void ToggleGlow(bool value){
        outline.enabled = value;
    }

    public void Interact(){
        if (!enabled) return;
        ToggleGlow(true);
        onInteract?.Invoke();
        if(!disableObject) return;
        Tween tween = transform.DOScale(Vector3.zero, 0.5f);
        tween.SetLink(gameObject);
        tween.OnComplete(() => gameObject.SetActive(false));
    }
}