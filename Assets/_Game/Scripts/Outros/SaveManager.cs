using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Encoders;

public class SaveManager {

    public SaveFile saveFile;

    public void SaveDataOnFile(){
        SaveGame.Encode = false;
        SaveGame.Save<SaveFile>("save", saveFile);
    }
}
