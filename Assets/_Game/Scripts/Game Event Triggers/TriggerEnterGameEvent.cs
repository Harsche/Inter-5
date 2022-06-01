using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterGameEvent : MonoBehaviour{
    [SerializeField] private UnityEvent onTriggerEnter;

    private void OnTriggerEnter(Collider other){
        if (!other.CompareTag("Player")) return;
        onTriggerEnter?.Invoke();
    }
}