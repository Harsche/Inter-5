using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;

public class TitleScreen : MonoBehaviour {
    [SerializeField] private GameObject continueOption;

    private void Awake(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SaveFile saveFile = SaveGame.Load<SaveFile>("save", (SaveFile)null, true);
        if (saveFile == null) {
            continueOption.SetActive(false);
            return;
        }
        SavePoint.saveFile = saveFile;
        continueOption.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(saveFile.curentScene));
    }

    public void DeleteSave(){
        SavePoint.saveFile = null;
    }
}