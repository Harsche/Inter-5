using UnityEngine;
using SUPERCharacter;

public class PlayerInteraction : MonoBehaviour{
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private SUPERCharacterAIO movement;
    [SerializeField] private Rigidbody playerRigidbody;
    private Transform myTransform;
    public static IInteractable interactable{ get; private set; }
    public static PlayerInteraction Instance{ get; private set; }
    

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
        myTransform = transform;
    }

    public void ToggleMovement(bool toggle){
        movement.controllerPaused = !toggle;
        playerRigidbody.velocity = Vector3.zero;
    }

    private void Update(){
        CheckForInteraction();
        if (Input.GetKeyDown(KeyCode.F) && interactable != null){
            interactable.Interact();
            interactable = null;
        }
    }

    private void CheckForInteraction(){
        var halfExtents = new Vector3(1f, 0.75f, 0.1f);
        Collider[] colliders = Physics.OverlapSphere(
            transform.position,
            1.5f,
            interactableLayer
        );


        /*Physics.BoxCast(
            myTransform.position,
            halfExtents,
            myTransform.forward,
            out RaycastHit hitInfo,
            myTransform.rotation,
            interactDistance,
            interactableLayer
        );*/

        //if (hitInfo.collider != null){
        //    interactable = hitInfo.collider.gameObject.GetComponent<IInteractable>();
        //    interactable.ToggleGlow(true);
        //}
        if (colliders.Length == 0)
            interactable = null;
        if (colliders.Length > 0 && interactable == null){
            interactable = colliders[0].gameObject.GetComponent<IInteractable>();
            interactable.ToggleGlow(true);
        }
    }
}

public interface IInteractable{
    public void Interact();
    public void ToggleGlow(bool value);
}