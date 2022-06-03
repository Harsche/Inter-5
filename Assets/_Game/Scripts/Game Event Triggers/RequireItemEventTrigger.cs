using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class RequireItemEventTrigger : MonoBehaviour, IInteractable{
    [SerializeField] private bool disableObject;
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
        if (this != PlayerInteraction.Interactable)
            ToggleGlow(false);
    }

    public void Interact(){
        if (!PlayerInteraction.godMode){
            if (Inventory.Instance.SelectedItem != requiredItem){
                missingItem?.Invoke();
                return;
            }

            if (!Inventory.Instance.RemoveItem(requiredItem)) return;
        }
        ToggleGlow(true);
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