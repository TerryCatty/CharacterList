using System;
using UnityEngine;

public class CellUI : ElementUI
{
	public Cell cell;
	
	public void SetCellItem()
	{
		cell.item.ObjectUI = this;
	}
	
	public void SetCell(Cell value)
	{
		cell = value;
	}
	
}

[Serializable]
public class Cell
{
	public Item item;
	
	public int count;
	public bool isTake => item != null;
}
