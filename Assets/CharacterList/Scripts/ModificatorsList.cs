using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ModificatorsList : Item
{
	public List<Modificator> modificators;
	
	
	public override void CheckBuff(IntParameter parameter)
	{
		try
		{
			int modificator = modificators.First(m => m.valueParameter.Contains(parameter.value)).valueModificator;
			
			modPlayer.AddBuff(modificator);
		}
		catch
		{
			Debug.Log("");
		}
	}

}

[Serializable]
public struct Modificator{
	public List<int> valueParameter;
	public int valueModificator;
}