using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class InteractionEventTrigger : MonoBehaviour, IInteractable{
    [SerializeField] private bool disableObject;
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private InteractionIcon interactionIcon;

    private Outline outline;
    private bool selected;

    private void Awake(){
        outline = GetComponent<Outline>();
        outline.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    private void Update(){
        if(!selected) return;
        if (this != PlayerInteraction.Interactable){
            selected = false;
            ToggleGlow(false);
        }
    }

    public void ToggleGlow(bool value){
        if (value) selected = true;
        outline.enabled = value;
        if(interactionIcon != null) interactionIcon.Toggle(value);
    }

    public bool InteractionEnabled(){
        return enabled;
    }

    public void Interact(){
        if (!enabled) return;
        ToggleGlow(true);
        if(disableObject) interactionIcon.Toggle(false);
        onInteract?.Invoke();
        if (!disableObject) return;
        Tween tween = transform.DOScale(Vector3.zero, 0.5f);
        tween.SetLink(gameObject);
        tween.OnComplete(() => gameObject.SetActive(false));
    }
}