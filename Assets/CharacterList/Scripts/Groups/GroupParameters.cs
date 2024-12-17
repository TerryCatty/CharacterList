using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class GroupParameters : Group
{
	public override void AddElement(string nameParameter, TypeElementGroup typeParam)
	{
		CreateElement(nameParameter, AllDictionary.instance.elementsDictionary.First(element => element.type == typeParam).prefab);
	}
	
	protected void CreateElement(string nameParameter, GroupElement prefab)
	{
		if(elements.Where(p => p.nameElement == nameParameter).Count() > 0) return;
		
		CharacterParameter newParameter = Instantiate(prefab.gameObject, transform.position, Quaternion.identity).GetComponent<CharacterParameter>();
		newParameter.SetName(nameParameter);
		newParameter.group = this;
		
		newParameter.transform.SetParent(transform);
		
		elements.Add(newParameter);
		
		CreateElementUI();
	}

	public override void CreateElementUI()
	{
		if(groupUI != null && groupUI.gameObject.activeInHierarchy == false) return;
		
		foreach(CharacterParameter parameter in elements)
		{
			if(parameter.ObjectUI == null)
				parameter.ObjectUI = groupUI.Create(parameter);
		}
	}
	
	
	
}
