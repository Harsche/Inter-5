using UnityEngine;
using UnityEngine.Events;

public class SceneStartEventTrigger : MonoBehaviour{
    [SerializeField] private UnityEvent onSceneStart;

    private void Start(){
        onSceneStart?.Invoke();
    }
}