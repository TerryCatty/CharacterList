using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ParameterUI_Number : ParameterUI
{
	[SerializeField] private GameObject editMenu;
	[SerializeField] private Toggle modificateToggle;
	
	
	[SerializeField] private float defalut_width;
	[SerializeField] private float max_height;
	[SerializeField] private float default_height;
	
	private PlayerRoll modPlayer;
	IntParameter intParameter;
	Toggle selectToggle;
	
	public override void Init()
	{
		base.Init();
		intParameter = parameter.GetComponent<IntParameter>();
		selectToggle = GetComponentInChildren<Toggle>();
		modPlayer = parameter.group.groupKeeper.GetComponent<PlayerRoll>();
	}
	
	public void EditMenu()
	{
		if(parameter.group.canEdit == false)
		{
			editMenu.SetActive(false);
			
			
			GetComponent<RectTransform>().sizeDelta = editMenu.activeInHierarchy ? 
			new Vector2(defalut_width, max_height) :
			new Vector2(defalut_width, default_height);
		
			return;
		}
		
		editMenu.SetActive(!editMenu.activeInHierarchy);
		
		GetComponent<RectTransform>().sizeDelta = editMenu.activeInHierarchy ? 
		new Vector2(defalut_width, max_height) :
		new Vector2(defalut_width, default_height);
		
		
		modificateToggle.isOn = intParameter.isModificate;
	}
	
	public void SelectToggle()
	{
		SelectParameter();
	}
	
	
	public void SelectParameter(bool changeFromParameter = false, bool value = false)
	{
		if(changeFromParameter == false)
		{
			if(intParameter.isModificate == false) 
			{
				selectToggle.isOn = false;
				modPlayer.SetParameter(null);
				intParameter.isChosen = selectToggle.isOn;
				
				return;
			}
			
			value = intParameter.isChosen;
		}	
		
		
		if(value || intParameter.isModificate == false)
		{
			modPlayer.SetParameter(null);
			selectToggle.isOn = false;
		}else
		{
			modPlayer.SetParameter(intParameter);
			selectToggle.isOn = true;
		}
		
		intParameter.isChosen = selectToggle.isOn;
		Debug.Log(intParameter.isChosen);
	}
	
	
	public void Modificate()
	{
		if(parameter.group.canEdit == false) return;
		
		intParameter.isModificate = modificateToggle.isOn;
		
		if(modificateToggle.isOn == false)
		{
			modPlayer.SetParameter(null);
			selectToggle.isOn = false;
			intParameter.isChosen = selectToggle.isOn;
		}
	}
}
