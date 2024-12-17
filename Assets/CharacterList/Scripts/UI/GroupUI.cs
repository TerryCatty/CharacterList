using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GroupUI : MonoBehaviour
{
	public List<ParameterUI> elementsUI;
	[SerializeField] private Transform scrollGameobject;
	
	[SerializeField] private TMP_InputField nameGroupText;
	
	[SerializeField] private GameObject CreatePanelParameterPrefab;
	private GameObject CreatePanelParameter;
	
	
	public Group group;
	
	private TMP_Dropdown dropdown;
	private TypeElementGroup typeParam;
	
	
	public void SetNameGroup(string value)
	{
		nameGroupText.text = value;
		nameGroupText.onValueChanged.AddListener(delegate {ChangeNameGroup(nameGroupText.text);});
	}
	
	public void ChangeNameGroup(string name)
	{
		group.SetName(name);
	}
	
	public void OpenWindow()
	{
		CreatePanelParameter = ManagerUI.instance.OpenWindow(CreatePanelParameterPrefab);
		
		InitParameter();	
	}
	
	public void InitParameter()
	{
		
		dropdown = CreatePanelParameter.GetComponentsInChildren<TMP_Dropdown>().ToList().First(but => but.gameObject.name == "ChooseType");
		dropdown.onValueChanged.AddListener(delegate{ChangeTypeDropdown();});
		SetCloseButton();
		
		SetCreateButton();
	}
	
	public void SetCloseButton()
	{
		Button closeButton = CreatePanelParameter.GetComponentsInChildren<Button>().ToList().First(but => but.gameObject.name == "CloseButton");
		
		closeButton.onClick.AddListener(delegate(){CloseWindow();});
	}
	
	public void SetCreateButton()
	{
		Button createButton = CreatePanelParameter.GetComponentsInChildren<Button>().ToList().First(but => but.gameObject.name == "CreateButton");
		TMP_InputField inputGroupName = CreatePanelParameter.GetComponentsInChildren<TMP_InputField>().ToList().First(but => but.gameObject.name == "InputName");
		
		createButton.onClick.AddListener(delegate(){CreateParameter(inputGroupName.text);});
	}
	
	public void ChangeTypeDropdown()
	{
		
	}
	
	public void CloseWindow()
	{
		ManagerUI.instance.CloseWindow(CreatePanelParameter);
	}
	
	public void CreateParameter(string nameParameter)
	{
		ChangeType();
		group.AddElement(nameParameter, typeParam);
	}
	
	public void ChangeType()
	{
		typeParam = AllDictionary.instance.elementsDictionary.First(par => par.type.ToString() == dropdown.options[dropdown.value].text.ToLower()).type;
	}
	public ParameterUI Create(CharacterParameter elementUI)
	{
		ParameterUI newElement = Instantiate(elementUI.prefabUI ,scrollGameobject);
		newElement.SetName(elementUI.nameParameter);
		newElement.parameter = elementUI;
		
		elementsUI.Add(newElement);
		
		return newElement;
	}
	
	public void DeleteGroup()
	{
		group.DeleteGroup();
	}
}
