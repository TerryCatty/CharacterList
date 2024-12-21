using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangingParameterUI : ElementUI
{
	
	[SerializeField] protected TMP_InputField nameText;
	[SerializeField] protected TMP_InputField valueText;
	[SerializeField] protected Toggle changeResult;
	[SerializeField] private ItemUI itemUI;
	
	private int id;
	
	public void SetItem(ItemUI item)
	{
		itemUI = item;
	}
	

	public override void SetName(string name)
	{
		base.SetName(name);
		
		nameText.text = name;
	}
	
	public void ChangeName()
	{
		ChangingParameter newParameter;
		newParameter = itemUI.getItem.parameters.First(p => p.id == id);
		newParameter.nameParameter = nameText.text;
		
		itemUI.ChangeItemParameter(id, newParameter);
	}
	
	public void ChangeIsChangeResult()
	{
		ChangingParameter newParameter;
		newParameter = itemUI.getItem.parameters.First(p => p.id == id);
		newParameter.changeResult = changeResult.isOn;
		
		itemUI.ChangeItemParameter(id, newParameter);
	}
	
	public void ChangeValueChange()
	{
		ChangingParameter newParameter;
		newParameter = itemUI.getItem.parameters.First(p => p.id == id);
		newParameter.changing = int.Parse(valueText.text);
		
		itemUI.ChangeItemParameter(id, newParameter);
	}
	
	public void DeleteParameter()
	{
		
	}

	public void SetId(int value)
	{
		id = value;
	}
}
