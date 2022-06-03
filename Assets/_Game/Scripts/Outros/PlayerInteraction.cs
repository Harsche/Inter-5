using UnityEngine;
using SUPERCharacter;

public class PlayerInteraction : MonoBehaviour{
    [SerializeField] private float interactRadius = 1f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private SUPERCharacterAIO movement;
    [SerializeField] private Rigidbody playerRigidbody;
    private Transform myTransform;
    public static IInteractable Interactable{ get; private set; }
    public SUPERCharacterAIO Movement => movement;
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
        if (Input.GetKeyDown(KeyCode.F) && Interactable != null){
            Interactable.Interact();
            Interactable = null;
        }
    }

    private void CheckForInteraction(){
        var halfExtents = new Vector3(1f, 0.75f, 0.1f);
        Collider[] colliders = Physics.OverlapSphere(
            transform.position,
            interactRadius,
            interactableLayer
        );
        
        if (colliders.Length == 0)
            Interactable = null;
        if (colliders.Length > 0 && Interactable == null){
            Interactable = colliders[0].gameObject.GetComponent<IInteractable>();
            Interactable.ToggleGlow(true);
        }
    }
}

public interface IInteractable{
    public void Interact();
    public void ToggleGlow(bool value);
}