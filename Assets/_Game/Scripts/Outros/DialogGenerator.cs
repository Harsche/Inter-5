using System;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog")]
public class DialogGenerator : ScriptableObject{
    [SerializeField] private DialogText[] dialogs;
    private string path;

    private void OnValidate(){
        path = Application.dataPath + "/_Game/Data/Dialogos/Novo_Dialogo.json";
    }

    public void GenerateJsonFile(){
        var serializeDialog = new SerializeDialog{
            dialog = dialogs
        };
        for (int i = 0; i < dialogs.Length; i++){
            string colorHex = ColorUtility.ToHtmlStringRGB(dialogs[i].dialogColor);
            serializeDialog.dialog[i].text = $"<color=#{colorHex}>" + serializeDialog.dialog[i].text;
            serializeDialog.dialog[i].text += "</color>";
        }
        File.WriteAllText(path, JsonUtility.ToJson(serializeDialog));
    }
}

[System.Serializable]
public class SerializeDialog{
    public DialogText[] dialog;
}