using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
public class Inventory : CharacterPart
{
   [SerializeField] private List<Item> cells;

   [SerializeField] private string nameInventory;
     public ItemType typeItem;

   [TextArea(15,20)]
    public string valueItem;

    private void Start(){
      
          switch(typeItem){
         case ItemType.Item:
            AddItem<Item>();
            break;
         case ItemType.ActionItem:
            AddItem<ActionItem>();
            break;
      }
    }

   public void AddItem<T>() where T : Item{
        T newItem = new GameObject("Item").AddComponent<T>();
        Item.SetData(valueItem, newItem);
        Debug.Log(Item.ToString(typeof(Item), newItem));
        cells.Add(newItem);
   }
     public void AddItem2<T>(string value) where T : Item{
        T newItem = new GameObject("Item").AddComponent<T>();
        newItem = Item.FromString(value, newItem);
        
        cells.Add(newItem);
   }
}


[Serializable] 
public enum ItemType{
   Item,
   ActionItem
}
