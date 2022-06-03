using UnityEngine;
using UnityEngine.SceneManagement;

public class Hacks : MonoBehaviour{
    [SerializeField] private string nextScene;

    private void Update(){
        if (Input.GetKeyDown(KeyCode.P)) SceneManager.LoadScene(nextScene);
        if (Input.GetKeyDown(KeyCode.O)) PlayerInteraction.godMode = !PlayerInteraction.godMode;
    }
}