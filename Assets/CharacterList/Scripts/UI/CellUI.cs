using System;
using UnityEngine;

public class CellUI : ElementUI
{
	public Cell cell;
	[SerializeField] private ItemUI itemPanel;
	
	private GameObject openedItemPanel;
	
	public void SetCellItem()
	{
		cell.item.ObjectUI = this;
	}
	
	public void SetCell(Cell value)
	{
		cell = value;
	}
	
	public void OpenItemPanel()
	{
		if(openedItemPanel != null || cell.item == null) return;
		
		openedItemPanel = ManagerUI.instance.OpenWindow(itemPanel.gameObject);
		
		openedItemPanel.GetComponent<ItemUI>().SetItem(cell.item);
	}
	
}

[Serializable]
public class Cell
{
	
	public int count;
	public bool isTake => item != null;
	public int idItem;
	public Item item;
}
