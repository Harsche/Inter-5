using UnityEngine;

[CreateAssetMenu(menuName = "Game Events")]
public class GameEvents : ScriptableObject{
    public void TriggerDialog(TextAsset dialogJson){
        Dialog.Instance.TriggerDialog(dialogJson);
    }

    public void CollectItem(Item item){
        Inventory.Instance.AddItem(item);
    }

    public void PlaySound(SoundPlayer soundPlayer){
        soundPlayer.PlaySound();
    }

    public void ViewItem(Item item){
        ItemDisplay.Instance.DisplayItem(item);
    }
}