using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Outline))]
public class NoteInteraction : MonoBehaviour, IInteractable{
    public Canvas canvas;
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
        canvas.enabled = true;
        PlayerInteraction.Instance.ToggleMovement(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Tween tween = transform.DOScale(Vector3.zero, 0.5f);
        tween.SetLink(gameObject);
        tween.OnComplete(() => {
            canvas.transform.SetParent(null);
            gameObject.SetActive(false);
        });
    }

    public void ToggleGlow(bool value){
        outline.enabled = value;
    }

    public void CloseMap(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerInteraction.Instance.ToggleMovement(true);
        canvas.enabled = false;
    }
}