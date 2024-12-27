using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class GroupParameters : Group
{
	public override void AddElement(string nameParameter, TypeElementGroup typeParam)
	{
		CreateElement(nameParameter, AllDictionary.instance.elementsDictionary.First(element => element.type == typeParam).prefab, typeParam);
	}
	
	protected void CreateElement(string nameParameter, GroupElement prefab, TypeElementGroup typeParam)
	{
		if(elements.Where(p => p.nameElement == nameParameter).Count() > 0) return;
		
		CharacterParameter newParameter = Instantiate(prefab.gameObject, transform.position, Quaternion.identity).GetComponent<CharacterParameter>();
		
		newParameter.SetParameters(countElements, nameParameter, this);
		
		newParameter.transform.SetParent(transform);
		elements.Add(newParameter);
		
		idElements newElement = new idElements();
		newElement.id = countElements;
		newElement.type = typeParam.ToString();
		idArray.Add(newElement);
		
		newParameter.Init();
		newParameter.SetParameters(newParameter.getId, nameParameter, this);
		
		
		countElements++;
		SaveData();
		
		CreateElementUI();
	}

	public override void CreateElementUI()
	{
		if(groupUI != null && groupUI.gameObject.activeInHierarchy == false) return;
		
		foreach(CharacterParameter parameter in elements)
		{
			if(parameter.ObjectUI == null)
				parameter.SetUI(groupUI.Create(parameter));
		}
	}

	public override void LoadData()
	{
		if(SaveManager.HasKey(SaveManager.instance.startFolder + path, "Group" + id))
		{
			string loadStr = SaveManager.GetString(SaveManager.instance.startFolder + path, "Group" + id);
			
			JsonUtility.FromJsonOverwrite(loadStr, this);
			elements.Clear();
			
			foreach(idElements group in idArray)
			{
				LoadElement(group.id, group.type);
			}
		}
	}
	
	private void LoadElement(int id, string type)
	{
		GroupElement prefab = AllDictionary.instance.elementsDictionary.First(element => element.type.ToString().ToLower() == type.ToLower()).prefab;
		
		
		CharacterParameter newParameter = Instantiate(prefab.gameObject, transform.position, Quaternion.identity).GetComponent<CharacterParameter>();
		newParameter.SetId(id);
		newParameter.group = this;
		newParameter.Init();
		
		
		newParameter.transform.SetParent(transform);
		
		elements.Add(newParameter);
		
		countElements++;
		
	}

}
