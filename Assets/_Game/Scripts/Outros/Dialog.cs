using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI dialogText;
    [HideInInspector] public event Action OnDialogEnd;
    
    private Coroutine dialogCoroutine;
    
    public static Dialog Instance{ get; private set; }

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void StopDialog(){
        if(dialogCoroutine != null) StopCoroutine(dialogCoroutine);
        dialogText.text = "";
    }

    public void TriggerDialog(TextAsset dialogJson){
        if(dialogCoroutine != null) StopCoroutine(dialogCoroutine);
        DialogText[] dialogs = JsonUtility.FromJson<SerializeDialog>(dialogJson.ToString()).dialog;
        dialogCoroutine = StartCoroutine(DisplayDialog(dialogs));
    }

    private IEnumerator DisplayDialog(DialogText[] dialogs){
        foreach (DialogText t in dialogs){
            dialogText.text = t.text;
            yield return new WaitForSeconds(t.time);
        }
        StopDialog();
        OnDialogEnd?.Invoke();
        OnDialogEnd = null;
    }
}

[System.Serializable]
public class DialogText{
    public string text;
    public float time;
    public Color dialogColor = Color.white;

    public DialogText(string text, float time){
        this.text = text;
        this.time = time;
    }
}