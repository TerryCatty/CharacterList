using TMPro;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;

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
		
		int count = item.parameters.Count;
		
		for(int i = 0; i < count; i++)
		{
			CreateUI(item.parameters[i].id, item.parameters[i]);
		}
	}
	
	public void ChangeName()
	{
		item.SetName(nameText.text);
		item.SaveData();
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
		item.SaveData();
		
		CreateUI(id);
	}
	private void CreateUI(int id, ChangingParameter parameter  = new ChangingParameter())
	{
		ChangingParameterUI newParameter = Instantiate(changingParameterUIPrefab, parametersKeeper.transform);
		
		
		newParameter.SetId(id);
		newParameter.SetItem(this);
		newParameter.SetParameter(parameter);
	}
	
	public void DeleteChangingParameter(int id)
	{
		item.RemoveChangingParameter(id);
		item.SaveData();
		
	}
	
	public void ChangeItemParameter(int id, ChangingParameter param)
	{
		item.ChangeParameter(id, param);
		item.SaveData();
	}
	
	public void ClosePanel()
	{
		ManagerUI.instance.CloseWindow(gameObject);
	}
	
}
