using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Group : MonoBehaviour
{
	[SerializeField] GroupUI groupPrefab;
	[SerializeField] GroupUI groupUI;
	[SerializeField] string _nameGroup;

	[SerializeField] private List<CharacterParameter> parameters;
	
	public GroupsKeeper groupKeeper;
	
	public string groupName => _nameGroup;

	public void SetName(string name)
	{
		_nameGroup = name;
	}

	public void AddParameter(string nameParameter, TypeParam typeParam)
	{
		CreateParameter(nameParameter, AllDictionary.instance.parametersDictionary.First(parameter => parameter.type == typeParam).prefab);
	}

	private void CreateParameter(string nameParameter, CharacterParameter prefab)
	{
		if(parameters.Where(p => p.nameParameter == nameParameter).Count() > 0) return;
		
		CharacterParameter newParameter = Instantiate(prefab.gameObject, transform.position, Quaternion.identity).GetComponent<CharacterParameter>();
		newParameter.SetName(nameParameter);
		newParameter.group = this;
		
		newParameter.transform.SetParent(transform);
		
		parameters.Add(newParameter);
		
		CreateParametersUI();
	}
	
	public void CreateParametersUI()
	{
		if(groupUI != null && groupUI.gameObject.activeInHierarchy == false) return;
		
		foreach(CharacterParameter parameter in parameters)
		{
			if(parameter.ObjectUI == null)
				parameter.ObjectUI = groupUI.Create(parameter);
		}
	}
	
	public void RemoveParameter(CharacterParameter parameter)
	{
		parameters.Remove(parameter);
	}
	
	public void DeleteGroup()
	{
		groupKeeper.RemoveGroup(this);
		Destroy(groupUI.gameObject);
		Destroy(gameObject);
	}
	
	public List<CharacterParameter> GetParameters() => parameters;
	public GroupUI prefab => groupPrefab;
	public GroupUI objectUI => groupUI;
	
	public void SetUI(GroupUI objectUI)
	{
		groupUI = objectUI;
		groupUI.group = this;
		groupUI.SetNameGroup(groupName);
	} 
}
