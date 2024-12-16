
using System;

public class IntParameter : CharacterParameter
{
	public int value;
	public bool isModificate;
	
	 public override void SetValue(string value)
	{
		this.value = Int32.Parse(value);
	}
}