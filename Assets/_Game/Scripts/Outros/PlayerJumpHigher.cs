using UnityEngine;

public class PlayerJumpHigher : MonoBehaviour{
    [SerializeField] private float multiplier = 1.25f;
    private float jump;

    private void Start(){
        jump = PlayerInteraction.Instance.Movement.jumpPower;
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("JumpHigher") && collision.GetContact(0).normal.z <= 0.95f){
            PlayerInteraction.Instance.Movement.jumpPower = jump * multiplier;
        }
    }

    private void OnCollisionExit(Collision other){
        if (other.gameObject.CompareTag("JumpHigher")){
            PlayerInteraction.Instance.Movement.jumpPower = jump;
        }
    }
}