using System;
using UnityEngine;

public class CharacterParameter : GroupElement
{
	[SerializeField] protected string nameTypeParameter;
	
	public string nameParameter => nameElement;
	public ParameterUI prefabUI;
	
	public ParameterUI ObjectUI;
	public Group group;

	public virtual void SetValue(string value)
	{
		
	}
	
	public void DeleteParameter()
	{
		group.RemoveElement(this);
		Destroy(ObjectUI.gameObject);
		Destroy(gameObject);
	}
}



public class BoolParameter : CharacterParameter
{
	public bool value;
}


[Serializable]
public enum TypeElementGroup
{
	text,
	number,
	boolean,
	defaultItem,
	actionItem
}
