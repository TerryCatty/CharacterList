using TMPro;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class ItemUI : ElementUI
{
   
	[SerializeField] protected TMP_InputField nameText;
   	public string nameItem => nameElement;
	
	[SerializeField] private Item item;
	
	public Item getItem => item;
	
	[SerializeField] private GameObject parametersKeeper;
	[SerializeField] private ChangingParameterUI changingParameterUIPrefab;
	
	
	public void SetItem(Item item)
	{
		this.item = item;
		
		nameElement = item.nameElement;
		nameText.text = nameElement;
	}
	
	public void ChangeName()
	{
		item.SetName(nameText.text);
	}
	
	public void DeleteItem()
	{
		item.group.RemoveElement(item);
		ClosePanel();
	}
	
	public void AddChangingParameter()
	{
		int id = item.idCount;
		item.idCount++;
		ChangingParameter parameter = new ChangingParameter();
		parameter.id = id;
		
		item.AddChangingParameter(parameter);
		
		CreateUI(id);
				
		RefreshParameters();
	}
	private void CreateUI(int id)
	{
		ChangingParameterUI newParameter = Instantiate(changingParameterUIPrefab, parametersKeeper.transform);
		
		
		newParameter.SetId(id);
		newParameter.SetItem(this);
	}
	
	public void DeleteChangingParameter(int id)
	{
		item.RemoveChangingParameter(id);
		
		RefreshParameters();
	}
	
	public void ChangeItemParameter(int id, ChangingParameter param)
	{
		item.ChangeParameter(id, param);
	}
	
	public void ClosePanel()
	{
		ManagerUI.instance.CloseWindow(gameObject);
	}
	private void RefreshParameters()
	{
		
	}
}
