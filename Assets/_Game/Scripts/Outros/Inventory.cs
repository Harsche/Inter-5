using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour{
    [SerializeField] private RectTransform selectedTransform;
    [SerializeField] private RectTransform lastSelectedTransform;

    private Image selectedItemImage;
    private Image lastItemImage;

    private int selectedIndex;
    private readonly List<Item> inventory = new List<Item>();

    public static Inventory Instance{ get; private set; }
    public Item SelectedItem => inventory[selectedIndex];

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
        if (Input.GetKeyDown(KeyCode.E)){
            selectedIndex++;
            if (selectedIndex > inventory.Count - 1) selectedIndex = 0;
            UpdateHUD();
        }
        else if (Input.GetKeyDown(KeyCode.Q)){
            selectedIndex--;
            if (selectedIndex < 0) selectedIndex = inventory.Count - 1;
            UpdateHUD();
        }
    }

    private void UpdateHUD(){
        if (inventory.Count == 0){
            selectedItemImage.enabled = false;
            return;
        }
        selectedItemImage.enabled = true;
        selectedItemImage.sprite = inventory[selectedIndex].itemSprites[0];
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