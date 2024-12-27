using UnityEngine;

public class GroupElement : MonoBehaviour, ISaveable
{
	[SerializeField] private string path;
   	public string nameElement;
	
	public ElementUI prefabUI;
	
	public ElementUI ObjectUI;
	public Group group;
	protected int id;
	
	public int getId => id;
	protected string keySave;
	
	public virtual void Init()
	{
		SaveManager.instance.AddSavingObject(this);
		LoadData();
	}
	
	public void SetParameters(int id, string name, Group group)
	{
		this.group = group;
		SetName(name);
		SetId(id);
		
		SaveData();
		
		Debug.Log(group.gameObject.name);
	}
	public void SetId(int id)
	{
		this.id = id;
	}
	
	public virtual void SetName(string name)
	{
		nameElement = name;
	}
	
	public virtual void SetValue(string value){

	}

	public virtual void SetUI(ElementUI ui)
	{
		ObjectUI = ui;
	}
	
	public virtual void DeleteElement(bool resetData)
	{
		if(resetData) ResetData();
		group.RemoveElement(this);
		if(ObjectUI != null) Destroy(ObjectUI.gameObject);
		Destroy(gameObject);
	}
	public void LoadData()
	{
		Group groupTemp = group;
		if(SaveManager.HasKey(SaveManager.instance.startFolder + group.getPath + path, keySave))
		{
			string loadStr = SaveManager.GetString(SaveManager.instance.startFolder + group.getPath + path, keySave);
			
			JsonUtility.FromJsonOverwrite(loadStr, this);
			Debug.Log(loadStr);
		}
		
		group = groupTemp;
	}
	
	public void SaveData()
	{
		string saveStr = JsonUtility.ToJson(this);
		SaveManager.instance.SetString(SaveManager.instance.startFolder + group.getPath + path, keySave, saveStr);
		
	}
	
	public void ResetData()
	{
		SaveManager.instance.DeleteKey(SaveManager.instance.startFolder + group.getPath + path, keySave);
		SaveManager.instance.RemoveSaveableObject(this);
	}
	
}
