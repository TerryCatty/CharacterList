using TMPro;
using UnityEngine;

public class ParameterUI : ElementUI
{
	[SerializeField] protected TMP_InputField nameText;
	[SerializeField] protected TMP_InputField valueText;
	
	
   	public string nameParameter => nameElement;
	
	public float width;
	public float height;
	
	public CharacterParameter parameter;
	
	public override void Init()
	{
		nameText.onValueChanged.AddListener(delegate {ChangeNameParameter(nameText.text);});
		valueText.onValueChanged.AddListener(delegate {ChangeValueParameter(valueText.text);});
	}
	
	public override void SetName(string name)
	{
		nameText.text = name;
	}
	
	public void ChangeNameParameter(string name)
	{
		if(parameter.group.canEdit == false) return;
		
		parameter.SetName(name);
	}
	
	public void SetValue(string value)
	{
		valueText.text = value;
	}
	
	public void ChangeValueParameter(string value)
	{
		parameter.SetValue(value);
	}
	
	public void DeleteParameter()
	{
		if(parameter.group.canEdit == false) return;
		
		parameter.DeleteElement();
	}
}
