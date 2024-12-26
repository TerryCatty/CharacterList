using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public interface ISaveable
{
	public void LoadData();
	public void SaveData();
	public void ResetData();
}
public class SaveManager : MonoBehaviour
{
	[SerializeField] private List<ISaveable> savingObjects = new List<ISaveable>();
	
	private const string saveExt = ".dnd";
	
	[SerializeField] private string start;
	
	public static SaveManager instance;
	
	private void Awake()
	{
		if(instance == null) instance = this;
		else Destroy(this);
	}
	
	public void AddSavingObject(ISaveable saveable)
	{
		savingObjects.Add(saveable);
	}
	
	public void RemoveSaveableObject(ISaveable saveable)
	{
		savingObjects.Remove(saveable);
	}
	
	public void Save()
	{
		foreach(ISaveable saveable in savingObjects)
		{
			saveable.SaveData();
		}
	}
	
	
	public void Load()
	{
		foreach(ISaveable saveable in savingObjects)
		{
			saveable.LoadData();
		}
	}
	
	public void ResetData(){
		PlayerPrefs.DeleteAll();
	}
	
	public static void SetString(string path, string key, string value)
	{
		string pathSave = path + key + saveExt;
		
		if(Directory.Exists(path) == false) 
			Directory.CreateDirectory(path);
		if (File.Exists(pathSave))
		{
			File.WriteAllText(pathSave, string.Empty);
		}
		else
		{
			File.Create(pathSave).Close();
		}
		
		File.WriteAllText(pathSave, value);
	}
	public static string GetString(string path, string key)
	{
		string value = "";
		string pathLoad = path + key + saveExt;
		
		if (File.Exists(pathLoad))
		{
			value = File.ReadAllText(pathLoad);
		}
		
		return value;
	}
	
	public static void DeleteKey(string path, string key)
	{
		string pathLoad = path + key + saveExt;
		if (File.Exists(pathLoad))
		{
			File.Delete(pathLoad);
		}
	}
	
	public static void DeleteAll()
	{
		
	}
	
	public static bool HasKey(string path, string key) => File.Exists(path + key + saveExt);
	
	public string startFolder => Application.persistentDataPath + start;
}
