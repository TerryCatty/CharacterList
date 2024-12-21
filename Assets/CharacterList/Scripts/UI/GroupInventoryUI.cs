using UnityEngine;
using Unity.VisualScripting;

public class GroupInventoryUI : GroupUI
{
	   public override ElementUI Create(GroupElement elementUI)
	{
		CellUI newElement = Instantiate(elementUI.prefabUI.GetComponent<CellUI>(), scrollGameobject);
		newElement.SetName(elementUI.nameElement);
		newElement.cell.item = elementUI.ConvertTo<Item>();
		
		elementsUI.Add(newElement);
		
		return newElement;
	}
	
	public override void InitElement()
	{
		
		SetCloseButton();
		
		SetCreateButton();
	}
	
	public override void CreateParameter(string nameParameter)
	{
		group.AddElement(nameParameter, TypeElementGroup.defaultItem);
		Debug.Log("element added");
	}
}
