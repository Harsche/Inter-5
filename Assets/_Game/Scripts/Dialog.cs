using System.Collections;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI dialogText;

    private Canvas dialogCanvas;
    private Coroutine dialogCoroutine;
    
    public static Dialog Instance{ get; private set; }

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
        dialogCanvas = GetComponent<Canvas>();
        dialogCanvas.enabled = false;
    }

    public void StopDialog(){
        if(dialogCoroutine != null) StopCoroutine(dialogCoroutine);
        dialogText.text = "";
        dialogCanvas.enabled = false;
    }

    public void TriggerDialog(TextAsset dialogJson){
        if(dialogCoroutine != null) StopCoroutine(dialogCoroutine);
        DialogText[] dialogs = JsonUtility.FromJson<SerializeDialog>(dialogJson.ToString()).dialog;
        dialogCanvas.enabled = true;
        dialogCoroutine = StartCoroutine(DisplayDialog(dialogs));
    }

    private IEnumerator DisplayDialog(DialogText[] dialogs){
        foreach (DialogText t in dialogs){
            dialogText.text = t.text;
            yield return new WaitForSeconds(t.time);
        }
        StopDialog();
    }
}

[System.Serializable]
public class DialogText{
    public string text;
    public float time;

    public DialogText(string text, float time){
        this.text = text;
        this.time = time;
    }
}