using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour{
    public static Inventory Instance;
    private static readonly List<Item> Items = new List<Item>();

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddItem(Item item){
        Items.Add(item);
    }

    public bool RemoveItem(string name){
        Item removedItem = Items.FirstOrDefault(i => i.itemName == name);
        if (removedItem == null) return false;
        Items.Remove(removedItem);
        return true;
    }
}
