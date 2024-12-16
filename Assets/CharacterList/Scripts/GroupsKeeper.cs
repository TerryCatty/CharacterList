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
	
	[SerializeField] private GameObject prefabGroupCreate;
	private GameObject GroupCreateUI;
	
	public void CreateGroup(string groupName)
	{
		if(characterGroups.Where(g => g.groupName == groupName).Count() > 0) return;
		
		
		Group newGroup = Instantiate(group.gameObject, transform.position, Quaternion.identity).GetComponent<Group>();
		
		newGroup.SetName(groupName);
		newGroup.groupKeeper = this;
		
		AddGroup(newGroup);
		newGroup.transform.SetParent(player.transform);
		
		RefreshGroups();
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
				group.CreateParametersUI();
			}
		}
	}
	
	public void OpenWindow()
	{
		GroupCreateUI = ManagerUI.instance.OpenWindow(prefabGroupCreate);
		
		SetCloseButton();
		SetCreateButton();
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
