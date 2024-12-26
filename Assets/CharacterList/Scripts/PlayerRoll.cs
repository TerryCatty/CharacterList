  using UnityEngine;
  using System.Linq;
using System.Collections.Generic;

public class PlayerRoll : MonoBehaviour
{
	[SerializeField] private int value;

	[SerializeField] private IntParameter modificationParameter;
	
	[SerializeField] private List<Item> buffsItems;
	
	private List<Item> deletedItems;
	int bonus;
	
	private void Awake()
	{
		deletedItems = new List<Item>();
	}

	public void AddItem(Item item)
	{
		buffsItems.Add(item);
	}
	
	public void RemoveItem(Item item)
	{
		deletedItems.Add(item);
	}


	public void SetParameter(IntParameter parameter)
	{
		if(parameter != null) modificationParameter?.SelectParameter();
		modificationParameter = parameter;
	}
	public void Roll()
	{
		CheckResult();
	}

	public void CheckResult()
	{
		Debug.Log("Roll");
		CheckBonus();
		
		if(modificationParameter != null)
		{
			modificationParameter.SelectParameter();
		}
		
		modificationParameter = null;
		
		if(deletedItems.Count > 0) buffsItems = buffsItems.Except(deletedItems).ToList();
	}
	
	
	private void CheckBonus()
	{
		CheckBuffs();
	}
	
	
	private void CheckBuffs()
	{
		foreach(Item buff in buffsItems)
		{
			buff.CheckBuff(modificationParameter);
		}
	}
	
	public void AddBuff(int buff)
	{
		bonus += buff;
		
		Debug.Log("+" + buff);
	}
	
	public void ChangeParameter(string nameParameter, int value)
	{
		List<Group> groupParameters = GetComponent<GroupsKeeper>().getGroups;
		
		foreach(Group group in groupParameters)
		{
			foreach(GroupElement elem in group.getElements)
			{
				if(elem.nameElement == nameParameter)
				{
					elem.GetComponent<IntParameter>().AddValue(value);
					return;
				}
			}
		}
	}
}
