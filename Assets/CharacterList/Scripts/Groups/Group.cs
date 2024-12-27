using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Group : MonoBehaviour, ISaveable
{
	[SerializeField] protected string path;
	[SerializeField] protected GroupUI groupPrefab;
	[SerializeField] protected GroupUI groupUI;
	[SerializeField] protected string _nameGroup;
	
	public GroupsKeeper groupKeeper;
	
	public string groupName => _nameGroup;
	
	[SerializeField] protected List<GroupElement> elements;
	
	protected GameObject creationPanel;
	
	[SerializeField] private bool canEditGroup;
	
	protected int id;
	public bool canEdit => canEditGroup;
	public List<GroupElement> getElements => elements;
	[SerializeField] protected List<idElements> idArray;
	protected int countElements;
	
	public int getId => id;
	
	public string getPath => path;
	
	public virtual void SetCreationPanel(GameObject panel)
	{
		creationPanel = panel;
	}
	
	public virtual void Init()
	{
		SaveManager.instance.AddSavingObject(this);
		LoadData();
	}

	public void SetId(int id)
	{
		this.id = id;
	}

	public void SetName(string name)
	{
		_nameGroup = name;
		SaveData();
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
	
	public void ResetData()
	{
		SaveManager.instance.DeleteKey(SaveManager.instance.startFolder + path, "Group" + id);
	}
	
	public virtual void RemoveElement(GroupElement element)
	{
		idArray.Remove(idArray.First(arr => arr.id == element.getId));
		elements.Remove(element);
	}
	
	public void DeleteGroup(bool resetData)
	{
		if(resetData) ResetData();
		SaveManager.instance.RemoveSaveableObject(this);
		groupKeeper.RemoveGroup(this);
		
		
		for(int i = 0; i < elements.Count; i++)
		{
			elements[i].DeleteElement(resetData);
		}
		
		
		if(groupUI != null) Destroy(groupUI.gameObject);
		
		Destroy(gameObject);
	}
	
	public GroupUI prefab => groupPrefab;
	public GroupUI objectUI => groupUI;
	
	public List<GroupElement> GetElements() => elements;
	
	
	public virtual void SetUI(GroupUI objectUI)
	{
		groupUI = objectUI;
		groupUI.group = this;
		groupUI.SetNameGroup(groupName);
	}

   public virtual void SaveData()
	{
		string saveStr = JsonUtility.ToJson(this);
		Debug.Log(saveStr + " - " + SaveManager.instance.startFolder + path + "Group" + id);
		SaveManager.instance.SetString(SaveManager.instance.startFolder + path, "Group" + id, saveStr);
		
	}
	
	public virtual void LoadData()
	{
		if(SaveManager.HasKey(SaveManager.instance.startFolder + path, "Group" + id))
		{
			string loadStr = SaveManager.GetString(SaveManager.instance.startFolder + path, "Group" + id);
			
			JsonUtility.FromJsonOverwrite(loadStr, this);
			elements.Clear();
		}
	}
}
