using System;
using UnityEngine;

public class CharacterParameter : GroupElement
{
	[SerializeField] protected string nameTypeParameter;
	
	public string nameParameter => nameElement;

	public virtual void SetValue(string value)
	{
		
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
