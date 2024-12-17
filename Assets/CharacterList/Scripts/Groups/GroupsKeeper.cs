using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class GroupsKeeper : MonoBehaviour
{
	[SerializeField] Transform groupsScroll;
	[SerializeField] Group group;
	[SerializeField] private GameObject player;
	[SerializeField] private List<Group> characterGroups;
	
	[SerializeField] private string strForCreated = "Created";
	
	[SerializeField] private GameObject prefabGroupCreate;
	private GameObject GroupCreateUI;
	
	private TMP_Dropdown dropdown;
	private GameObject createdParameters;
	private List<Transform> listCreateParameters;
		
	public void CreateGroup(string groupName)
	{
		if(characterGroups.Where(g => g.groupName == groupName).Count() > 0) return;
		
		
		ChangeType();
		
		Group newGroup = Instantiate(group.gameObject, transform.position, Quaternion.identity).GetComponent<Group>();
		
		newGroup.SetName(groupName);
		newGroup.SetCreationPanel(GroupCreateUI);
		newGroup.groupKeeper = this;
		
		AddGroup(newGroup);
		newGroup.transform.SetParent(player.transform);
		
		RefreshGroups();
	}
	
	private void ChangeType()
	{
		group = AllDictionary.instance.groupsDictionary.First(group => group.type.ToLower() == dropdown.options[dropdown.value].text.ToLower()).prefab;
	}
	
	
	public void AddGroup(Group group)
	{
		characterGroups.Add(group);
	}
	
	public void RemoveGroup(Group group)
	{
		characterGroups.Remove(group);
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
		
		createButton.onClick.AddListener(delegate(){CreateGroup(inputGroupName.text);});
	}
	public void CloseWindow()
	{
		ManagerUI.instance.CloseWindow(GroupCreateUI);
	}
}
