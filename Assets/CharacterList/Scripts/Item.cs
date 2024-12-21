using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Item : GroupElement
{
	public string description;
	public int maxStack;
	
	public List<ChangingParameter> parameters;
	

	public static void SetData<T>(string itemParameters, T item){
		List<string> parameters = itemParameters.Split("\n").ToList();
		parameters.Remove(parameters[parameters.Count - 1]);


		Type type = typeof(T);


		foreach(string param in parameters){
			string parameter = param.Split(":")[0].Replace(" ", "");
			string value = param.Split(":")[1];

			
			try{
				Type fieldType = type.GetField(parameter).FieldType;
				type.GetField(parameter).SetValue(item, Convert.ChangeType(value, fieldType));
			}
			catch{
				Debug.LogWarning("Item not contain this parameter: " + parameter);
			}
		}
	}

	public static string ToString<T>(Type type, T item)
	{
		string result = "";

		foreach(var f in type.GetFields().Where(f => f.IsPublic)){
			result += $"{f.Name}:{f.GetValue(item)};";
		}

		return result;
	}

	
	public static T FromString<T>(string parames, T item){
		Type type = typeof(T);
		List<string> parameters = parames.Split("\n").ToList();
		parameters.Remove(parameters[parameters.Count - 1]);


		foreach(string param in parameters){
			string parameter = param.Split(":")[0].Replace(" ", "");
			string value = param.Split(":")[1];

			try{
				Type fieldType = type.GetField(parameter).FieldType;
				type.GetField(parameter).SetValue(item, Convert.ChangeType(value, fieldType));
			}
			catch{
				Debug.LogWarning("Item not contain this parameter: " + parameter);
			}
		}

		return item;
	}
}

[Serializable]
public struct ChangingParameter
{
	public string parameter;
	public bool changeResult;
	public bool changeParameter;
	public int changing;
}