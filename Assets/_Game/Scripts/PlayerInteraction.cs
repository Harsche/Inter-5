using UnityEngine;

public class PlayerInteraction : MonoBehaviour{
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactableLayer;
    private Transform myTransform;
    private IInteractable interactable;

    private void Awake(){
        myTransform = transform;
    }

    private void Update(){
        CheckForInteraction();
        if (Input.GetKeyDown("e") && interactable != null){
            interactable.Interact();
            interactable = null;
        }
    }

    private void CheckForInteraction(){
        var halfExtents = new Vector3(1f, 0.75f, 0.1f);
        Physics.BoxCast(
            myTransform.position,
            halfExtents,
            myTransform.forward,
            out RaycastHit hitInfo,
            myTransform.rotation,
            interactDistance,
            interactableLayer
        );

        if (hitInfo.collider != null){
            interactable = hitInfo.collider.gameObject.GetComponent<IInteractable>();
        }
        
    }
}

public interface IInteractable{
    public void Interact();
}