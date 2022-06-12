using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Inventory : MonoBehaviour{
    [SerializeField] private RectTransform selectedTransform;
    [SerializeField] private RectTransform lastSelectedTransform;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private Image background;

    private Image selectedItemImage;
    private Image lastItemImage;

    private int selectedIndex;
    private readonly List<Item> inventory = new List<Item>();

    public static Inventory Instance{ get; private set; }
    public Item SelectedItem => inventory.Count >= 1 ? inventory[selectedIndex] : null;

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
        selectedIndex = 0;
        selectedItemImage = selectedTransform.GetComponent<Image>();
        //lastItemImage = lastItem.GetComponent<Image>();

        UpdateHUD();
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            selectedIndex++;
            if (selectedIndex > inventory.Count - 1) selectedIndex = 0;
            UpdateHUD();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)){
            selectedIndex--;
            if (selectedIndex < 0) selectedIndex = inventory.Count - 1;
            UpdateHUD();
        }
    }

    private void UpdateHUD(){
        UpdateArrows();
        if (inventory.Count == 0){
            selectedItemImage.enabled = false;
            background.DOFade(0f, 1f);
            return;
        }
        background.DOFade(1f, 1f);
        selectedItemImage.enabled = true;
        selectedItemImage.sprite = inventory[selectedIndex].itemSprites[0];
    }
    
    private void UpdateArrows(){
        leftArrow.SetActive(inventory.Count > 1);
        rightArrow.SetActive(inventory.Count > 1);
    }

    public void AddItem(Item item){
        inventory.Add(item);
        selectedIndex = inventory.Count - 1;
        UpdateHUD();
    }

    public bool RemoveItem(Item item){
        if (!inventory.Contains(item)) return false;
        inventory.Remove(item);
        selectedIndex = 0;
        UpdateHUD();
        return true;
    }
}