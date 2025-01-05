  using UnityEngine;
  using System.Linq;
using System.Collections.Generic;

public class PlayerRoll : MonoBehaviour
{
	[SerializeField] private int value;

	[SerializeField] private IntParameter modificationParameter;
	[SerializeField] private RollWindow rollWindowPrefab;
	
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
		bonus = 0;

        CheckResult();
	}

	public void CheckResult()
	{
		Debug.Log("Roll");

        CheckMod();

        RollWindow rollWindow = ManagerUI.instance.OpenWindow(rollWindowPrefab.gameObject).GetComponent<RollWindow>();
		rollWindow.SetText(bonus.ToString());
		rollWindow.SetPlayerRoll(this);
	}
	
	
	private void CheckBuffs()
	{
		foreach(Item buff in buffsItems)
		{
			buff.CheckBuff(modificationParameter);
		}
	}
    private void CheckMod()
    {
        foreach (Item buff in buffsItems)
        {
            buff.CheckMod(modificationParameter);
        }
    }

    public void AddMod(int mod)
	{
		bonus += mod;
		
		Debug.Log("+" + mod);
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

	public void ConfirmRoll(bool applyBuffs)
	{
		if(applyBuffs)
            CheckBuffs();



        if (modificationParameter != null)
        {
            modificationParameter.SelectParameter();
        }

        modificationParameter = null;

        if (deletedItems.Count > 0) buffsItems = buffsItems.Except(deletedItems).ToList();
    }
}
