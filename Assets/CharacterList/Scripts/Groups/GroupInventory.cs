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
	
	[SerializeField] private List<Cell> listCells;
	
	[SerializeField] private CellUI prefabCell;
	
	public override void AddElement(string nameItem, TypeElementGroup typeItem)
	{
		Item item = AllDictionary.instance.elementsDictionary.First(item => item.type == typeItem).prefab.ConvertTo<Item>();
		CreateElement(nameItem, item);
	}

	protected void CreateElement(string nameItem, Item prefab)
	{
		Item newItem = Instantiate(prefab.gameObject, transform.position, Quaternion.identity).GetComponent<Item>();
		newItem.SetName(nameItem);
		newItem.group = this;
		
		newItem.transform.SetParent(transform);
		
		elements.Add(newItem);
		
		AddItem(newItem);
	}
	
	private void AddItem(Item item)
	{
		try
		{
			int targetIndex = listCells.IndexOf(listCells.First(cell => cell.item == null || 
			(cell.item.nameElement == item.nameElement && cell.item.maxStack < cell.count && cell.item.GetType() == item.GetType())));
			
			Debug.Log(targetIndex);
			
			listCells[targetIndex].item = item;
			listCells[targetIndex].count++;
			
			groupUI.elementsUI[targetIndex].GetComponent<CellUI>().SetCell(listCells[targetIndex]);
			
			Debug.Log(listCells[targetIndex].count);
		}
		
		catch
		{
			Debug.Log("NO one cell");
		}
	}
	
	
	

	public override void SetCreationPanel(GameObject panel)
	{
		base.SetCreationPanel(panel);
		
		SetCapacity();
	}
	
	public void SetCapacity()
	{
		TMP_InputField inputCapacity = creationPanel.GetComponentsInChildren<TMP_InputField>().ToList().First(but => but.gameObject.name == nameInputCapacity);
		
		capacityInventory = 0;
		
		try
		{
			capacityInventory = Int32.Parse(inputCapacity.text);
		}
		catch
		{
			capacityInventory = 18;
		}
		
		for(int i = listCells.Count; i < capacityInventory; i++)
		{
			listCells.Add(new Cell());
			groupUI.CreateOnlyUI(prefabCell);
		}
	}
}
