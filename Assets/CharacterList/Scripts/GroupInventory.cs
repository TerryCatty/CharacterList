using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GroupInventory : Group
{
	private List<Item> items;
	
	  
	public override void AddElement(string nameItem, TypeElementGroup typeItem)
	{
		Item item = AllDictionary.instance.elementsDictionary.First(item => item.type == typeItem).prefab.ConvertTo<Item>();
		CreateElement(nameItem, item);
	}

	protected void CreateElement(string nameItem, Item prefab)
	{
		
	}
	
	public override void CreateElementUI()
	{
		
	}
	
	public void RemoveElement(Item item)
	{
		items.Remove(item);
	}
	
	public List<Item> GetElements() => items;
}
