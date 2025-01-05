using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ParameterUI : ElementUI
{
	[SerializeField] protected TextMeshProUGUI nameText;
	[SerializeField] protected TextMeshProUGUI valueText;
    [SerializeField] protected TMP_InputField nameChangeText;
    [SerializeField] protected TMP_InputField valueChangeText;
    [SerializeField] protected GameObject[] editMenu;


    public string nameParameter => nameElement;
	
	public float width;
	public float height;

    protected bool isEdit;

    public CharacterParameter parameter;
	
	public virtual new void Init()
	{

	}

    public virtual void EditMenu()
    {
        if (parameter.group.canEdit == false)
        {
            isEdit = false;


            foreach (GameObject go in editMenu)
            {
                go.SetActive(isEdit);
            }

            SetValuesEditMenu();

            SetSize();


            return;
        }

        isEdit = !isEdit;

        foreach (GameObject go in editMenu)
        {
            go.SetActive(isEdit);
        }

        SetValuesEditMenu(true);

        SetSize();
    }

	protected virtual void SetSize()
	{

	}

    protected virtual void SetValuesEditMenu(bool requireConfirm = false)
    {

    }

    public override void SetName(string name)
	{
		nameText.text = name;
	}
	
	public void ChangeNameParameter(string name)
	{
		if(parameter.group.canEdit == false) return;
		
		parameter.SetName(name);
		parameter.SaveData();
	}
	
	public void SetValue(string value)
	{
		valueText.text = value;
	}
	
	public void ChangeValueParameter(string value)
	{
		parameter.SetValue(value);
		parameter.SaveData();
	}
	
	public void DeleteParameter()
	{
		if(parameter.group.canEdit == false) return;
		
		parameter.RemoveElement(true);
	}
}
