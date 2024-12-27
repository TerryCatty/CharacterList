
using UnityEngine;

public class StringParameter : CharacterParameter, ISaveable
{
	public string value;
	
	string key;
	
	private ParameterUI textUI;

	public override void SetValue(string value)
	{
		this.value = value;
		
		textUI.SetValue(value);
		SaveData();
	}

	public override void SetName(string name)
	{
		base.SetName(name);
		
		textUI?.SetName(name);
		SaveData();
	}
	public override void SetUI(ElementUI element)
	{
		base.SetUI(element);
		textUI = ObjectUI.GetComponent<ParameterUI>();
		SetValue(value.ToString());
		SetName(nameElement);
	}
	
	public override void Init()
	{
		keySave = "NumberParameter" + id + "FromGroup" + group.getId;
		base.Init();
	}
	// public override void DeleteElement()
	// {
	// 	ResetData();
	// 	base.DeleteElement();
		
	// }
	
}
