using UnityEngine;
using DG.Tweening;

public class MapInteraction : MonoBehaviour, IInteractable{
    public Canvas map;
    private Outline outline;

    private void Awake(){
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }
    
    private void Update(){
        if(this != PlayerInteraction.interactable)
            ToggleGlow(false);
    }

    public void Interact(){
        map.enabled = true;
        Tween tween = transform.DOScale(Vector3.zero, 0.5f);
        tween.SetLink(gameObject);
        tween.OnComplete(() => {
            map.transform.SetParent(null);
            gameObject.SetActive(false);
        });
    }

    public void ToggleGlow(bool value){
        outline.enabled = value;
    }

    public void CloseMap(){
        map.enabled = false;
    }
}