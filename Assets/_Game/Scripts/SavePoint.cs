using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            SaveFile.playerPosition = other.transform.position;
            SaveFile.curentScene = SceneManager.GetActiveScene().name;
        }
    }
}
