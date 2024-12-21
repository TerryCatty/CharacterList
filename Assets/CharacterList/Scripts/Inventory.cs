using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
public class Inventory : CharacterPart
{
	[SerializeField] private List<Item> items;

	[SerializeField] private string nameInventory;

	[TextArea(15,20)]
	 public string valueItem;


	public void AddItem<T>() where T : Item{
		  T newItem = new GameObject("Item").AddComponent<T>();
		  Item.SetData(valueItem, newItem);
		  Debug.Log(Item.ToString(typeof(Item), newItem));
		  items.Add(newItem);
	}
	  public void AddItem2<T>(string value) where T : Item{
		  T newItem = new GameObject("Item").AddComponent<T>();
		  newItem = Item.FromString(value, newItem);
		  
		  items.Add(newItem);
	}
}

