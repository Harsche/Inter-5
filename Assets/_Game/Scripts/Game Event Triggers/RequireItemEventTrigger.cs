using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class RequireItemEventTrigger : MonoBehaviour, IInteractable{
    [SerializeField] private bool disableObject;
    [SerializeField] private bool consumeItem = true;
    [SerializeField] private Item requiredItem;
    [SerializeField] private UnityEvent onUseItem;
    [SerializeField] private UnityEvent missingItem;

    private Outline outline;

    private void Awake(){
        outline = GetComponent<Outline>();
        outline.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    private void Update(){
        if (!enabled){
            ToggleGlow(false);
            return;
        }

        if (this != PlayerInteraction.Interactable)
            ToggleGlow(false);
    }

    private void OnDisable(){
        ToggleGlow(false);
    }
    
    public bool InteractionEnabled(){
        return enabled;
    }

    public void Interact(){
        if (!enabled) return;
        if (!PlayerInteraction.godMode){
            if (Inventory.Instance.SelectedItem != requiredItem){
                missingItem?.Invoke();
                return;
            }
            
            if(consumeItem) Inventory.Instance.RemoveItem(requiredItem);
        }

        ToggleGlow(false);
        onUseItem?.Invoke();
        if (!disableObject) return;
        Tween tween = transform.DOScale(Vector3.zero, 0.5f);
        tween.SetLink(gameObject);
        tween.OnComplete(() => gameObject.SetActive(false));
    }

    public void ToggleGlow(bool value){
        outline.enabled = value;
    }
}