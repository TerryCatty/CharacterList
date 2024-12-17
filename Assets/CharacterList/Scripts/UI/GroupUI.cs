using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GroupUI : MonoBehaviour
{
	public List<ElementUI> elementsUI;
	[SerializeField] private Transform scrollGameobject;
	
	[SerializeField] private TMP_InputField nameGroupText;
	
	[SerializeField] private GameObject CreatePanelParameterPrefab;
	private GameObject CreatePanelParameter;
	
	
	public Group group;
	
	private TMP_Dropdown dropdown;
	private TypeElementGroup typeElement;
	
	
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
		
		InitElement();	
	}
	
	public void InitElement()
	{
		
		dropdown = CreatePanelParameter.GetComponentsInChildren<TMP_Dropdown>().ToList().First(but => but.gameObject.name == "ChooseType");
		
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
		Debug.Log("Create button is init");
		
		Button createButton = CreatePanelParameter.GetComponentsInChildren<Button>().ToList().First(but => but.gameObject.name == "CreateButton");
		TMP_InputField inputGroupName = CreatePanelParameter.GetComponentsInChildren<TMP_InputField>().ToList().First(but => but.gameObject.name == "InputName");
		
		createButton.onClick.AddListener(delegate(){CreateParameter(inputGroupName.text);});
		Debug.Log("listener added");
	}
	
	public void CloseWindow()
	{
		ManagerUI.instance.CloseWindow(CreatePanelParameter);
	}
	
	public void CreateParameter(string nameParameter)
	{
		ChangeType();
		group.AddElement(nameParameter, typeElement);
		Debug.Log("element added");
	}
	
	public void ChangeType()
	{
		typeElement = AllDictionary.instance.elementsDictionary.First(par => par.type.ToString() == dropdown.options[dropdown.value].text.ToLower()).type;
		Debug.Log("type changed");
	}
	public virtual ElementUI Create(GroupElement elementUI)
	{
		ParameterUI newElement = Instantiate(elementUI.prefabUI.GetComponent<ParameterUI>(), scrollGameobject);
		newElement.SetName(elementUI.nameElement);
		newElement.parameter = elementUI.ConvertTo<CharacterParameter>();;
		
		elementsUI.Add(newElement);
		
		return newElement;
	}
	
	public void DeleteGroup()
	{
		group.DeleteGroup();
	}
}
