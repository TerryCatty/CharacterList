using System;
using UnityEngine;

public class CharacterParameter : MonoBehaviour
{
	[SerializeField] protected string _nameParameter;
	[SerializeField] protected string nameTypeParameter;
	
	public string nameParameter => _nameParameter;
	public ParameterUI prefabUI;
	
	public ParameterUI ObjectUI;
	public Group group;

	public void SetName(string name)
	{
		_nameParameter = name;
	}
	
	public virtual void SetValue(string value)
	{
		
	}
	
	public void DeleteParameter()
	{
		group.RemoveParameter(this);
		Destroy(ObjectUI.gameObject);
		Destroy(gameObject);
	}
}



public class BoolParameter : CharacterParameter
{
	public bool value;
}


[Serializable]
public enum TypeParam
{
	text,
	number,
	boolean
}
