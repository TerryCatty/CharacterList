using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Item : GroupElement
{
	public string description;
	public int maxStack;
	
	public List<ChangingParameter> parameters;
	
	public int idCount;
	[SerializeField] private bool removeAfterRoll = true;
	[SerializeField] protected PlayerRoll modPlayer;

	public override void Init()
	{
		keySave = $"Item_{id}";
		
		
		base.Init();
	}

	private void Start()
	{
		if(group != null)
			modPlayer = group.groupKeeper.GetComponent<PlayerRoll>();
	}
	
	public void AddChangingParameter(ChangingParameter param)
	{
		parameters.Add(param);
	}
	
	public void RemoveChangingParameter(int id)
	{
		parameters.Remove(parameters.First(p => p.id == id));
	}
	
	public void ChangeParameter(int index, ChangingParameter param)
	{
		parameters[parameters.IndexOf(parameters.First(p => p.id == index))] = param;
	}
	
	public virtual void CheckBuff(IntParameter parameter)
	{
			foreach(ChangingParameter param in parameters)
			{
				if(parameter == null)
				{
					if(!param.changeResult) modPlayer.ChangeParameter(param.nameParameter, param.changing);
				}
				else
				{
					if(param.nameParameter.ToLower() == parameter.nameElement)
					{
						if(param.changeResult) modPlayer.AddBuff(param.changing);
						else modPlayer.ChangeParameter(param.nameParameter, param.changing);
					}
				}
				
			}
		
		if(removeAfterRoll) modPlayer.RemoveItem(this);
	}

}

[Serializable]
public struct ChangingParameter
{
	public int id;
	public string nameParameter;
	public bool changeResult;
	public int changing;
	
	
}