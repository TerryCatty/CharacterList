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
	
	public string getStart => start;
	
	public static SaveManager instance;
	
	private saveList _saveList;
	
	public List<SavePath> savePaths => _saveList.listSave;
	
	private void Awake()
	{
		if(instance == null) instance = this;
		else Destroy(this);
		
		_saveList.listSave = new List<SavePath>();
		Load();
	}
	
	public void SetStartFolder(string pathFolder){
		start = pathFolder;
	}
	
	public void AddSavingObject(ISaveable saveable)
	{
		if(savingObjects.Contains(saveable) == false) savingObjects.Add(saveable);
	}
	
	public void RemoveSaveableObject(ISaveable saveable)
	{
		savingObjects.Remove(saveable);
	}
	
	
	public void SaveAll()
	{
		foreach(ISaveable saveable in savingObjects)
		{
			saveable.SaveData();
		}
	}
	
	private void AddPath(SavePath save)
	{
		if( _saveList.listSave.Where(s => s.path == save.path).Count() <= 0)  _saveList.listSave.Add(save);
	}
	private void DeletePath(SavePath save)
	{
		if( _saveList.listSave.Where(s => s.path == save.path).Count() <= 0)  _saveList.listSave.Remove(save);
	}
	private void SavePath()
	{
		string saveStr = JsonUtility.ToJson( _saveList);
		SaveSaveManager(Application.persistentDataPath, "SavesPath", saveStr);
		
	}
	private void Load()
	{
		string loadStr = GetString(Application.persistentDataPath, "SavesPath");
		
		JsonUtility.FromJsonOverwrite(loadStr,  _saveList);
	}
	
	public void ResetData(){
		PlayerPrefs.DeleteAll();
	}
	
	public void SetString(string path, string key, string value)
	{
		string pathSave = path + key + saveExt;
		
		if(Directory.Exists(path) == false) 
			Directory.CreateDirectory(path);
		if (File.Exists(pathSave) == false)
		{
			File.Create(pathSave).Close();
		}
		
		File.WriteAllText(pathSave, value);
		
		SavePath newSavePath = new SavePath();
		newSavePath.name = start;
		newSavePath.path = Application.persistentDataPath + "/" + start + "/";
		
		AddPath(newSavePath);
		SavePath();
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
	
	public void DeleteKey(string path, string key)
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
	
	private void SaveSaveManager(string path, string key, string value)
	{
		string pathSave = path + key + saveExt;
		
		if(Directory.Exists(path) == false) 
			Directory.CreateDirectory(path);
		if (File.Exists(pathSave) == false)
		{
			File.Create(pathSave).Close();
		}
		
		File.WriteAllText(pathSave, value);
		
	}
	
	public static bool HasKey(string path, string key) => File.Exists(path + key + saveExt);
	
	public string startFolder => Application.persistentDataPath + "/" + start + "/";
}

[Serializable]
public struct SavePath
{
	public string path;
	public string name;
}

[Serializable]
public struct saveList
{
	public List<SavePath> listSave;
}