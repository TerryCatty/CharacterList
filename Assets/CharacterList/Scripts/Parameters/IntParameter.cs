
using System;
using UnityEngine;

public class IntParameter : CharacterParameter
{
	public int value;
	public bool isModificate;
	
	public bool isChosen;
	
	private ParameterUI_Number parameterUI;
	
	public override void SetUI(ElementUI element)
	{
		base.SetUI(element);
		parameterUI = ObjectUI.GetComponent<ParameterUI_Number>();
		SetValue(value.ToString());
	}
	
	public override void Init()
	{
		keySave = "NumberParameter" + id + "FromGroup" + group.getId;
		base.Init();
	}
	
	 public override void SetValue(string value)
	{
		this.value = Int32.Parse(value);
		
		parameterUI?.SetValue(value);
	}
	
	public void AddValue(int value)
	{
		this.value += value;
		parameterUI.SetValue(this.value.ToString());
	}
	
	public void SelectParameter()
	{
		parameterUI.SelectParameter(true, isChosen);
	}

	public override void DeleteElement()
	{
		ResetData();
		base.DeleteElement();
		
	}

	
}