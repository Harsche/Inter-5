using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour{
    [SerializeField] private Image displayImage;
    
    public static ItemDisplay Instance{ get; private set; }

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void DisplayItem(Item item){
        displayImage.sprite = item.itemSprite;
        ToggleDisplay(true);
    }

    public void ToggleDisplay(bool value){
        for (int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).gameObject.SetActive(value);
        }
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = value;
        if(!value) displayImage = null;
        PlayerInteraction.Instance.ToggleMovement(!value);
    }
}