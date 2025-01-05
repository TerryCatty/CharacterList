
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class IntParameter : CharacterParameter
{
	public int value;
	public bool isModificate;
	
	public bool isChosen;
	
	private ParameterUI_Number parameterUI;

	bool tempMod;
	
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
		SaveData();
	}

    public override void SetName(string name)
    {
        base.SetName(name);
        parameterUI?.SetName(name);
        SaveData();
    }

    public void AddValue(int value)
	{
		this.value += value;
		parameterUI.SetValue(this.value.ToString());
		SaveData();
	}
	
	public void SelectParameter()
	{
		parameterUI.SelectParameter(true, isChosen);
	}


    public virtual void RequireSetMod(bool value)
    {
        tempMod = value;
        confirmAction += ConfirmSetMod;
    }
    private void ConfirmSetMod()
    {
        SetMod(tempMod);
    }

	public void SetMod(bool value)
	{
		isModificate = value;
	}
}