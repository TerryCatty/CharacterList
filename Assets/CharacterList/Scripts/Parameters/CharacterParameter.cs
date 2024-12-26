using System;
using UnityEngine;

public class CharacterParameter : GroupElement
{
	
	public string nameParameter => nameElement;


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
