using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Group : MonoBehaviour, ISaveable, IConfirmable
{
	[SerializeField] protected string path;
	[SerializeField] protected GroupUI groupPrefab;
	[SerializeField] protected GroupUI groupUI;
	
	public GroupsKeeper groupKeeper;

	[SerializeField] protected SaveGroupData saveData;

    public string groupName => saveData.name;
	
	[SerializeField] protected List<GroupElement> elements;
	
	protected GameObject creationPanel;
	
	protected int id;
	public bool canEdit => saveData.canEditGroup;
	public List<GroupElement> getElements => elements;
	
	public int getId => id;
	
	public string getPath => path;


	private Action confirmAction;
    private GroupElement removableElementTemp;
	private string nameTemp;

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
        saveData.name = name;
		groupUI?.SetNameGroup(name);
		SaveData();
	}

	public void RequireSetName(string name, bool isConfirmRequire = false)
	{
		if (isConfirmRequire)
        {
			nameTemp = name;
            confirmAction += ConfirmSetName;

            ManagerUI.instance.ConfirmWindow("Change name?", this);
        }
		else
		{
			SetName(name);
		}
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
	
	public virtual void RemoveElement(GroupElement element, bool resetData = true)
	{
		removableElementTemp = element;

		if(resetData == false)
        {
			Debug.Log("Remove");
            if (saveData.idArray.Where(arr => arr.id == element.getId).Count() > 0)
            {
                saveData.idArray.Remove(saveData.idArray.First(arr => arr.id == element.getId));
            }
            removableElementTemp = null;
            elements.Remove(element);

            element.DeleteElement(element);

        }
		else
		{
			confirmAction += ConfirmDeleteElemet;

            ManagerUI.instance.ConfirmWindow("Delete this parameter?", this);
		}
	}
	
	public void RequestDeleteGroup(bool resetData, bool confirmRequest = false)
	{
		if (confirmRequest)
		{
            confirmAction += ConfirmDeleteGroup;

            ManagerUI.instance.ConfirmWindow("Delete this group?", this);
        }
		else
		{
			DeleteGroup(resetData);
		}
	}

	private void DeleteGroup(bool resetData)
	{

        SaveManager.instance.RemoveSaveableObject(this);
        groupKeeper.RemoveGroup(this, resetData);


        for (int i = 0; i < elements.Count; i++)
        {
            elements[i].DeleteElement(resetData);
        }


        if (groupUI != null) Destroy(groupUI.gameObject);

        Destroy(gameObject);
    }

	private void ConfirmDeleteGroup()
	{
		DeleteGroup(true);
        ResetData();
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
		string saveStr = JsonUtility.ToJson(saveData);

		Debug.Log(saveStr);

		SaveManager.instance.SetString(SaveManager.instance.startFolder + path, "Group" + id, saveStr);
		
	}
	
	public virtual void LoadData()
	{
		Debug.Log(SaveManager.instance.startFolder + path + "Group" + id);

		if(SaveManager.HasKey(SaveManager.instance.startFolder + path, "Group" + id))
		{
			string loadStr = SaveManager.GetString(SaveManager.instance.startFolder + path, "Group" + id);
            Debug.Log(loadStr);

            saveData = JsonUtility.FromJson<SaveGroupData>(loadStr);
			elements.Clear();
		}
	}

    public void Confirm()
    {
		confirmAction?.Invoke();
		confirmAction = null;
    }

    public void Cancel()
    {
        
    }

	private void ConfirmDeleteElemet()
	{
        if (saveData.idArray.Where(arr => arr.id == removableElementTemp.getId).Count() > 0)
        {
            saveData.idArray.Remove(saveData.idArray.First(arr => arr.id == removableElementTemp.getId));
        }
        elements.Remove(removableElementTemp);
        removableElementTemp.DeleteElement(removableElementTemp);

        removableElementTemp = null;
        SaveData();
    }

	private void ConfirmSetName()
	{
		SetName(nameTemp);
    }
}

[Serializable]
public struct SaveGroupData
{
	public string name;
	public List<idElements> idArray;
	public bool canEditGroup;
    public int countElements;
}


