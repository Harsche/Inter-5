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
    [SerializeField] private InteractionIcon interactionIcon;

    private Outline outline;
    private bool selected;

    private void Awake(){
        outline = GetComponent<Outline>();
        outline.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    private void Update(){
        if (!selected) return;
        if (!enabled){
            ToggleGlow(false);
            return;
        }

        if (this != PlayerInteraction.Interactable){
            selected = false;
            ToggleGlow(false);
        }
    }

    private void OnDisable(){
        ToggleGlow(false);
    }
    
    public bool InteractionEnabled(){
        return enabled;
    }

    public void Interact(){
        if (!enabled){
            Debug.Log("DEBUG 1");
            return;
        }
        if (!PlayerInteraction.godMode){
            if (Inventory.Instance.SelectedItem != requiredItem){
                missingItem?.Invoke();
                return;
            }
            
            if(consumeItem) Inventory.Instance.RemoveItem(requiredItem);
        }

        ToggleGlow(false);
        if(disableObject) interactionIcon.Toggle(false);
        onUseItem?.Invoke();
        Debug.Log("DEBUG 2");
        if (!disableObject) return;
        Debug.Log("DEBUG 3");
        Tween tween = transform.DOScale(Vector3.zero, 0.5f);
        tween.SetLink(gameObject);
        tween.OnComplete(() => gameObject.SetActive(false));
    }

    public void ToggleGlow(bool value){
        if (value) selected = true;
        outline.enabled = value;
        if(interactionIcon != null) interactionIcon.Toggle(value);
    }
}