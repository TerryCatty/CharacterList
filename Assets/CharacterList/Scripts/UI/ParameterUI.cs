using TMPro;
using UnityEngine;

public class ParameterUI : MonoBehaviour
{
	protected string nameParameter;
	
	[SerializeField] protected TMP_InputField nameText;
	[SerializeField] protected TMP_InputField valueText;
	
	public float width;
	public float height;
	
	public CharacterParameter parameter;
	
	public void SetName(string name)
	{
		nameParameter = name;
		nameText.text = name;
		nameText.onValueChanged.AddListener(delegate {ChangeNameParameter(nameText.text);});
		valueText.onValueChanged.AddListener(delegate {ChangeValueParameter(valueText.text);});
	}
	
	public void ChangeNameParameter(string name)
	{
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
		parameter.DeleteParameter();
	}
}
