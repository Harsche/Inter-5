using UnityEngine;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Encoders;

public class SavePoint : MonoBehaviour {
    public static SaveFile saveFile;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if(saveFile == null)
                saveFile = new SaveFile();
            saveFile.playerPosition = other.transform.position;
            saveFile.curentScene = SceneManager.GetActiveScene().name;
            SaveDataOnFile();
        }
    }

    public void SaveDataOnFile(){
        SaveGame.Encode = true;
        SaveGame.Save<SaveFile>("save", saveFile);
    }
}
