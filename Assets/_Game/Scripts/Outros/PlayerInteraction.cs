using System;
using UnityEngine;
using SUPERCharacter;

public class PlayerInteraction : MonoBehaviour{
    [SerializeField] private float interactDistance = 1f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private SUPERCharacterAIO movement;
    [SerializeField] private Rigidbody playerRigidbody;

    public SUPERCharacterAIO Movement => movement;
    public Rigidbody PlayerRigidbody => playerRigidbody;
    private float walkingSpeed;
    
    public static IInteractable Interactable{ get; private set; }
    public static PlayerInteraction Instance{ get; private set; }
    
    public static bool godMode;
    

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start(){
        walkingSpeed = movement.walkingSpeed;
    }

    public void ToggleMovement(bool toggle){
        movement.controllerPaused = !toggle;
        playerRigidbody.velocity = Vector3.zero;
    }
    
    public void ToggleWalk(bool toggle){
        movement.walkingSpeed = toggle ? walkingSpeed : 0f;
        playerRigidbody.velocity = Vector3.zero;
    }

    private void Update(){
        CheckForInteraction();
        if (Input.GetKeyDown(KeyCode.E) && Interactable != null){
            Interactable.Interact();
            Interactable = null;
        }
    }

    private void CheckForInteraction(){
        Transform cameraTransform = movement.playerCamera.transform;
        Physics.Raycast(
            cameraTransform.position,
            cameraTransform.forward,
            out RaycastHit hitInfo,
            interactDistance,
            interactableLayer
        );
        
        if (hitInfo.collider ==  null) Interactable = null;
        if (hitInfo.collider != null && Interactable == null){
            Interactable = hitInfo.collider.gameObject.GetComponent<IInteractable>();
            if(Interactable != null && Interactable.InteractionEnabled()) Interactable.ToggleGlow(true);
        }
    }
}

public interface IInteractable{
    public void Interact();
    public void ToggleGlow(bool value);
    public bool InteractionEnabled();
}