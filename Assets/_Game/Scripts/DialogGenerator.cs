using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog")]
public class DialogGenerator : ScriptableObject{
    [SerializeField] private DialogText[] dialogs;
    private const string Path = "D:/Projetos Unity/Inter 5/Assets/_Game/Data/Dialogos/Novo_Dialogo.json";
    
    public void GenerateJsonFile(){
        var serializeDialog = new SerializeDialog{
            dialog = dialogs
        };
        File.WriteAllText(Path, JsonUtility.ToJson(serializeDialog));
    }
}

[System.Serializable]
public class SerializeDialog{
    public DialogText[] dialog;
}