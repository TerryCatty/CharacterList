using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GroupUI : MonoBehaviour
{
	public List<ElementUI> elementsUI;
	[SerializeField] protected Transform scrollGameobject;
	
	[SerializeField] protected TMP_InputField nameGroupText;
	
	[SerializeField] protected GameObject CreatePanelParameterPrefab;
	protected GameObject CreatePanelParameter;
	
	
	public Group group;
	
	protected TMP_Dropdown dropdown;
	protected TypeElementGroup typeElement;
	
	
	public void SetNameGroup(string value)
	{
		nameGroupText.text = value;
		nameGroupText.onValueChanged.AddListener(delegate {ChangeNameGroup(nameGroupText.text);});
	}
	
	public void ChangeNameGroup(string name)
	{
		if(group.canEdit == false) return;
		
		group.SetName(name);
	}
	
	public void OpenWindow()
	{
		if(group.canEdit == false) return;
		
		if(CreatePanelParameter != null) return;
		
		CreatePanelParameter = ManagerUI.instance.OpenWindow(CreatePanelParameterPrefab);
		
		InitElement();	
	}
	
	public virtual void InitElement()
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
	
	public virtual void CreateParameter(string nameParameter)
	{
		if(group.canEdit == false) return;
		
		if(nameParameter.Replace(" ", "") == "") return;
		
		ChangeType();
		group.AddElement(nameParameter, typeElement);
		Debug.Log("element added");
	}
	
	public void ChangeType()
	{
		string equalseText = dropdown.options[dropdown.value].text.ToLower().Replace(" ", "");
		
		typeElement = AllDictionary.instance.elementsDictionary.First(par => par.type.ToString().ToLower() == equalseText).type;
		Debug.Log("type changed");
	}
	public virtual ElementUI Create(GroupElement elementUI)
	{
		ParameterUI newElement = Instantiate(elementUI.prefabUI.GetComponent<ParameterUI>(), scrollGameobject);
		newElement.SetName(elementUI.nameElement);
		newElement.parameter = elementUI.ConvertTo<CharacterParameter>();;
		newElement.Init();
		
		elementsUI.Add(newElement);
		
		return newElement;
	}
	
	public virtual ElementUI CreateOnlyUI<T>(T elementUI) where T : ElementUI
	{
		T newElement = Instantiate(elementUI.gameObject, scrollGameobject).GetComponent<T>();
		
		newElement.Init();
		
		elementsUI.Add(newElement);
		
		return newElement;
	}
	
	public void DeleteGroup()
	{
		group.DeleteGroup(true);
	}
}
