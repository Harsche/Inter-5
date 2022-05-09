using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour{
    [SerializeField] private string sceneName;
    
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
            SceneManager.LoadScene(sceneName);
    }
}
