using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Group : MonoBehaviour
{
	[SerializeField] protected GroupUI groupPrefab;
	[SerializeField] protected GroupUI groupUI;
	[SerializeField] protected string _nameGroup;
	
	public GroupsKeeper groupKeeper;
	
	public string groupName => _nameGroup;
	
	[SerializeField] protected List<GroupElement> elements;
	
	protected GameObject creationPanel;
	
	public virtual void SetCreationPanel(GameObject panel)
	{
		creationPanel = panel;
	}


	public void SetName(string name)
	{
		_nameGroup = name;
	}

	public virtual void AddElement(string nameItem, TypeElementGroup typeItem)
	{
		
	}

	protected virtual void CreateElement(string nameParameter, CharacterParameter prefab)
	{
		
	}
	
	public virtual void CreateElementUI()
	{
		
	}
	
	public virtual void RemoveElement(GroupElement element)
	{
		elements.Remove(element);
	}
	
	public void DeleteGroup()
	{
		groupKeeper.RemoveGroup(this);
		Destroy(groupUI.gameObject);
		Destroy(gameObject);
	}
	
	public GroupUI prefab => groupPrefab;
	public GroupUI objectUI => groupUI;
	
	public List<GroupElement> GetElements() => elements;
	
	
	public void SetUI(GroupUI objectUI)
	{
		groupUI = objectUI;
		groupUI.group = this;
		groupUI.SetNameGroup(groupName);
	} 
}
