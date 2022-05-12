using UnityEngine;
using DG.Tweening;


[RequireComponent(typeof(Outline))]
public class Collectable : MonoBehaviour, IInteractable{
    [SerializeField] private Item item;
    private Outline outline;

    private void Awake(){
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void Update(){
        if (this != PlayerInteraction.interactable)
            ToggleGlow(false);
    }

    public void Interact(){
        Inventory.Instance.AddItem(item);
        ToggleGlow(true);
        Tween tween = transform.DOScale(Vector3.zero, 0.5f);
        tween.SetLink(gameObject);
        tween.OnComplete(() => gameObject.SetActive(false));
    }

    public void ToggleGlow(bool value){
        outline.enabled = value;
    }
}