using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ParameterUI_Number : ParameterUI
{
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
	

    protected override void SetSize()
    {
        GetComponent<RectTransform>().sizeDelta = isEdit ?
        new Vector2(defalut_width, max_height) :
        new Vector2(defalut_width, default_height);
    }

    protected override void SetValuesEditMenu(bool requireConfirm = false)
    {
        if(isEdit) modificateToggle.isOn = intParameter.isModificate;

        bool tempMod = modificateToggle.isOn;


        if (!requireConfirm)
		{
            intParameter.SetName(nameChangeText.text);
            intParameter.SetValue(valueChangeText.text);
			intParameter.SetMod(tempMod);
        }
		else
		{
            if (nameChangeText.text != nameText.text || valueChangeText.text != valueText.text)
            {
                if (isEdit == false)
				{
                    intParameter.RequireSetName(nameChangeText.text);
                    intParameter.RequireSetValue(valueChangeText.text);
                    intParameter.RequireSetMod(tempMod);
					intParameter.OpenConfirmPanel();
                }

			}
        }
		

        nameChangeText.text = nameText.text;
		valueChangeText.text = valueText.text;
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
	
	
	public void ModificateOpen()
	{
		if(parameter.group.canEdit == false) return;
		
		if(modificateToggle.isOn == false)
		{
			modPlayer.SetParameter(null);
			selectToggle.isOn = false;
			intParameter.isChosen = selectToggle.isOn;
		}
	}

}
