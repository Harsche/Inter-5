using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Outline))]
public class RequireItem : MonoBehaviour, IInteractable{
    [SerializeField] private string requiredItem;
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
        if (!Inventory.Instance.RemoveItem(requiredItem)) return;
        ToggleGlow(true);
        Tween tween = transform.DOScale(Vector3.zero, 0.5f);
        tween.SetLink(gameObject);
        tween.OnComplete(() => gameObject.SetActive(false));
        Debug.Log("SUCESSO");
    }

    public void ToggleGlow(bool value){
        outline.enabled = value;
    }
}