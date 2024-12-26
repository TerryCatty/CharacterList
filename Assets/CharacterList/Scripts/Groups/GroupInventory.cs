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
	
	[SerializeField] private CellsJson cells;
	
	[SerializeField] private CellUI prefabCell;

	public override void SetUI(GroupUI objectUI)
	{
		base.SetUI(objectUI);
		
		SetCapacity(capacityInventory);
		
		
			for(int i = 0; i < cells.listCells.Count; i++)
			{
				if(cells.listCells[i].count > 0)
					LoadItem(cells.listCells[i].idItem, i);
			}
	}

	public override void AddElement(string nameItem, TypeElementGroup typeItem)
	{
		Item item = AllDictionary.instance.elementsDictionary.First(item => item.type == typeItem).prefab.ConvertTo<Item>();
		CreateElement(nameItem, item);
	}

	protected void CreateElement(string nameItem, Item prefab)
	{
		
		int targetIndex = cells.listCells.IndexOf(cells.listCells.First(cell => cell.item == null || 
			(cell.item.nameElement == nameItem && cell.item.maxStack > cell.count && cell.item.GetType() == prefab.GetType())));
			
		if(cells.listCells[targetIndex]?.item?.nameElement == nameItem 
		&& cells.listCells[targetIndex]?.item?.maxStack > cells.listCells[targetIndex].count)
		{
			cells.listCells[targetIndex].count++;
		}
		else
		{	
			
			Item newItem = Instantiate(prefab.gameObject, transform.position, Quaternion.identity).GetComponent<Item>();
			newItem.SetName(nameItem);
			countElements++;
			
			newItem.SetId(countElements);
			
			newItem.group = this;
			newItem.Init();
			
			newItem.transform.SetParent(transform);
			
			elements.Add(newItem);
			
			AddItem(newItem, targetIndex);
		}
		
	}
	
	private void AddItem(Item item, int idCell)
	{
		try
		{
			int targetIndex = idCell;
			
			
			cells.listCells[targetIndex].item = item;
			cells.listCells[targetIndex].count++;
			
			cells.listCells[targetIndex].idItem = countElements;
			
			groupUI.elementsUI[targetIndex].GetComponent<CellUI>().SetCell(cells.listCells[targetIndex]);
			
		}
		
		catch
		{
			Debug.Log("NO one cell");
		}
	}
	
	
	

	public override void SetCreationPanel(GameObject panel)
	{
		base.SetCreationPanel(panel);
		try
		{
			TMP_InputField inputCapacity = creationPanel.GetComponentsInChildren<TMP_InputField>().ToList().First(but => but.gameObject.name == nameInputCapacity);
		
		
			try
			{
				SetCapacity(Int32.Parse(inputCapacity.text));
			}
			catch
			{
				SetCapacity(18);
			}
		}
		catch
		{
			
		}
		
	}
	
	public void SetCapacity(int capacity)
	{
		capacityInventory = capacity;
		
		if(cells.listCells.Count < capacity)
		{
			for(int i = cells.listCells.Count; i < capacityInventory; i++)
			{
				cells.listCells.Add(new Cell());
				groupUI.CreateOnlyUI(prefabCell);
			}
		}
		else
		{
			for(int i = 0; i < capacityInventory; i++)
			{
				groupUI.CreateOnlyUI(prefabCell);
			}
		}
		
	}
	
	
   public override void SaveData()
	{
		string saveStr = JsonUtility.ToJson(this);
		SaveManager.SetString(SaveManager.instance.startFolder + path, "Group" + id, saveStr);
		
		saveStr = JsonUtility.ToJson(cells);
		SaveManager.SetString(SaveManager.instance.startFolder + path, "Group" + id + "Cells", saveStr);
		
		PlayerPrefs.Save();
	}
	
	public override void LoadData()
	{
		if(SaveManager.HasKey(SaveManager.instance.startFolder + path, "Group" + id))
		{
			string loadStr = SaveManager.GetString(SaveManager.instance.startFolder + path, "Group" + id);
			
			JsonUtility.FromJsonOverwrite(loadStr, this);
			
			loadStr = SaveManager.GetString(SaveManager.instance.startFolder + path, "Group" + id + "Cells");
			JsonUtility.FromJsonOverwrite(loadStr, cells);
			
			elements.Clear();
			
		}
	}
	
	private void LoadItem(int id, int cellIndex)
	{ 
		Item prefab = AllDictionary.instance.elementsDictionary.First(item => item.type == TypeElementGroup.defaultItem).prefab.ConvertTo<Item>();
		
		Item newItem = Instantiate(prefab.gameObject, transform.position, Quaternion.identity).GetComponent<Item>();
		newItem.SetId(id);
		
		newItem.group = this;
		newItem.Init();
		newItem.group = this;
		
		
		newItem.transform.SetParent(transform);
		
		elements.Add(newItem);
		
		
		int targetIndex = cellIndex;
		cells.listCells[targetIndex].item = newItem;
		
		LoadCellUI(targetIndex);
	}
	
	private void LoadCellUI(int targetIndex)
	{
		groupUI.elementsUI[targetIndex].GetComponent<CellUI>().SetCell(cells.listCells[targetIndex]);
	}
}

[Serializable]
public struct CellsJson
{
	 public List<Cell> listCells;
}
