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

}

