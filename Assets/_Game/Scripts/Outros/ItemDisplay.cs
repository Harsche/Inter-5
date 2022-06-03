using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour{
    [SerializeField] private Image displayImage;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;

    private int selectedSprite;
    private Item currentItem;
    public bool displayingItem{ get; private set; }

    public static ItemDisplay Instance{ get; private set; }

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void UpdateArrows(){
        leftArrow.SetActive(selectedSprite > 0);
        rightArrow.SetActive(selectedSprite < currentItem.itemSprites.Length - 1);
    }

    public void ChangeSprite(int addIndex){
        selectedSprite += addIndex;
        selectedSprite = Mathf.Clamp(selectedSprite, 0, currentItem.itemSprites.Length - 1);
        displayImage.sprite = currentItem.itemSprites[selectedSprite];
        UpdateArrows();
    }

    public void DisplayItem(Item item){
        selectedSprite = 0;
        currentItem = item;
        displayImage.sprite = currentItem.itemSprites[selectedSprite];
        UpdateArrows();
        ToggleDisplay(true);
    }

    public void ToggleDisplay(bool value){
        displayingItem = value;
        for (int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).gameObject.SetActive(value);
        }

        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = value;
        if (!value) displayImage.sprite = null;
        PlayerInteraction.Instance.ToggleMovement(!value);
    }
}