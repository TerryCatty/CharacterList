using UnityEngine;
using UnityEngine.UI;

public class ParameterUI_Number : ParameterUI
{
	[SerializeField] private GameObject editMenu;
	[SerializeField] private Toggle modificateToggle;
	
	
	[SerializeField] private float defalut_width;
	[SerializeField] private float max_height;
	[SerializeField] private float default_height;
	
	private ModificatorsPlayer modPlayer;
	
	private void Start()
	{
		modPlayer = parameter.group.groupKeeper.GetComponent<ModificatorsPlayer>();
		modificateToggle.onValueChanged.AddListener(delegate{Modificate();});
	}
	
	public void EditMenu()
	{
		
		editMenu.SetActive(!editMenu.activeInHierarchy);
		
		GetComponent<RectTransform>().sizeDelta = editMenu.activeInHierarchy ? 
		new Vector2(defalut_width, max_height) :
		new Vector2(defalut_width, default_height);
		
		
		modificateToggle.isOn = parameter.GetComponent<IntParameter>().isModificate;
	}
	
	public void ChooseParameter()
	{
		if(parameter.GetComponent<IntParameter>().isModificate == false || editMenu.activeInHierarchy) return;
		
		modPlayer.SetParameter(parameter.GetComponent<IntParameter>());
	}
	
	private void Modificate()
	{
		parameter.GetComponent<IntParameter>().isModificate = modificateToggle.isOn;
	}
}
