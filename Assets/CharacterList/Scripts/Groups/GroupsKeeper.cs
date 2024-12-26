using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using System;

public class GroupsKeeper : MonoBehaviour, ISaveable
{
	[SerializeField] Transform groupsScroll;
	[SerializeField] Group group;
	[SerializeField] private GameObject player;
	[SerializeField] private List<Group> characterGroups;
	public List<Group> getGroups => characterGroups;
	
	[SerializeField] private string strForCreated = "Created";
	
	[SerializeField] private GameObject prefabGroupCreate;
	private GameObject GroupCreateUI;
	
	private TMP_Dropdown dropdown;
	private GameObject createdParameters;
	private List<Transform> listCreateParameters;
	
	[SerializeField] private List<idElements> idArray;
	private int countGroups;
	
	private void Start()
	{
		SaveManager.instance.AddSavingObject(this);
		LoadData();
	}
	
		
	public void CreateGroup(string groupName, int id, bool ignoreName = false, string type = "")
	{
		if(ignoreName == false)
		{
			if(characterGroups.Where(g => g.groupName == groupName).Count() > 0) return;
			
			if(groupName.Replace(" ", "") == "") return;
		
		}
		
		if(type == "") type = dropdown.options[dropdown.value].text;
		ChangeType(type);
		
		Group newGroup = Instantiate(group.gameObject, transform.position, Quaternion.identity).GetComponent<Group>();
		
		newGroup.SetName(groupName);
		newGroup.groupKeeper = this;
		newGroup.SetId(id);
		
		
		idElements newId = new idElements();
		newId.id = id;
		newId.type = type;
		idArray.Add(newId);
		AddGroup(newGroup);
		
		newGroup.Init();
		
		newGroup.transform.SetParent(player.transform);
		
		RefreshGroups();
		
		newGroup.SetCreationPanel(GroupCreateUI);
		
		countGroups++;
		
	}
	
	private void ChangeType(string type)
	{
		group = AllDictionary.instance.groupsDictionary.First(group => group.type.ToLower() == type.ToLower()).prefab;
	}
	
	
	public void AddGroup(Group group)
	{
		characterGroups.Add(group);
	}
	
	public void RemoveGroup(Group group)
	{
		characterGroups.Remove(group);
		idArray.Remove(idArray.First(arr => arr.id == group.getId));
	}
	
	public void RefreshGroups()
	{
		foreach(Group group in characterGroups)
		{
			if(group.objectUI == null)
			{
				GroupUI newUI = Instantiate(group.prefab.gameObject, groupsScroll.transform).GetComponent<GroupUI>();
				group.SetUI(newUI);
				group.CreateElementUI();
			}
		}
	}
	
	public void OpenWindow()
	{
		if(GroupCreateUI != null) return;
		
		GroupCreateUI = ManagerUI.instance.OpenWindow(prefabGroupCreate);
		
		InitParameter();	
	}
	public void InitParameter()
	{
		
		dropdown = GroupCreateUI.GetComponentsInChildren<TMP_Dropdown>().ToList().First(but => but.gameObject.name == "ChooseType");
		dropdown.onValueChanged.AddListener(delegate{ChangeTypeDropdown();});
		
		listCreateParameters = GroupCreateUI.GetComponentsInChildren<Transform>().ToList().Where(obj => obj.gameObject.name.Contains(strForCreated)).ToList();
		
		ChangeTypeDropdown();
		
		SetCloseButton();
		
		SetCreateButton();
	}
	
	
	public void ChangeTypeDropdown()
	{
		try
		{
			createdParameters = listCreateParameters.
			First(but => but.gameObject.name.ToLower() == (dropdown.options[dropdown.value].text  + strForCreated).ToLower()).gameObject;
			
			foreach(Transform obj in listCreateParameters)
			{
				if(obj.gameObject != createdParameters)
					obj.gameObject.SetActive(false);
				else
					createdParameters.SetActive(true);
			}
		
			
		}
		catch
		{
			Debug.LogError("No one gameobject with name " + dropdown.options[dropdown.value].text);
		}
		
	}
	
	
	
	public void SetCloseButton()
	{
		Button closeButton = GroupCreateUI.GetComponentsInChildren<Button>().ToList().First(but => but.gameObject.name == "CloseButton");
		
		closeButton.onClick.AddListener(delegate(){CloseWindow();});
	}
	
	
	public void SetCreateButton()
	{
		Button createButton = GroupCreateUI.GetComponentsInChildren<Button>().ToList().First(but => but.gameObject.name == "CreateButton");
		TMP_InputField inputGroupName = GroupCreateUI.GetComponentsInChildren<TMP_InputField>().ToList().First(but => but.gameObject.name == "InputName");
		
		createButton.onClick.AddListener(delegate(){CreateGroup(inputGroupName.text, countGroups);});
	}
	public void CloseWindow()
	{
		ManagerUI.instance.CloseWindow(GroupCreateUI);
	}
	
	public void ResetData()
	{
		SaveManager.DeleteKey(SaveManager.instance.startFolder, "GroupKeeper");
	}
	
	public void SaveData()
	{
		string saveStr = JsonUtility.ToJson(this);
		SaveManager.SetString(SaveManager.instance.startFolder, "GroupKeeper", saveStr);
		
		//PlayerPrefs.Save();
	}
	
	public void LoadData()
	{
		if(SaveManager.HasKey(SaveManager.instance.startFolder, "GroupKeeper"))
		{
			string loadStr = SaveManager.GetString(SaveManager.instance.startFolder, "GroupKeeper");
			
			JsonUtility.FromJsonOverwrite(loadStr, this);
			countGroups = 0;
			characterGroups.Clear();
			
			foreach(idElements group in idArray)
			{
				LoadGroup(group.id, group.type);
			}
		}
	}
	
	public void LoadGroup(int id, string type = "")
	{
		if(type == "") type = dropdown.options[dropdown.value].text;
		ChangeType(type);
		
		Group newGroup = Instantiate(group.gameObject, transform.position, Quaternion.identity).GetComponent<Group>();
		
		newGroup.groupKeeper = this;
		newGroup.SetId(id);
		
		newGroup.Init();
		
		
		AddGroup(newGroup);
		newGroup.transform.SetParent(player.transform);
		
		RefreshGroups();
		newGroup.SetCreationPanel(GroupCreateUI);
		
		countGroups++;
		
	}
}

[Serializable]
public struct idElements
{
	public int id;
	public string type;
	
}

