using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GroupInventory : Group
{
	[SerializeField] private int capacityInventory;
	
	[SerializeField] private string nameInputCapacity;
	
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

	public override void SetCreationPanel(GameObject panel)
	{
		base.SetCreationPanel(panel);
		
		SetCapacity();
	}
	
	public void SetCapacity()
	{
		TMP_InputField inputCapacity = creationPanel.GetComponentsInChildren<TMP_InputField>().ToList().First(but => but.gameObject.name == nameInputCapacity);
		capacityInventory = Int32.Parse(inputCapacity.text);
	}
}
