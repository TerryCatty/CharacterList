using System;
using System.Collections.Generic;
using UnityEngine;

public class AllDictionary : MonoBehaviour
{
	public List<ParameterType> parametersDictionary;
	public static AllDictionary instance;
	
	public void Awake()
	{
		if(instance != null) Destroy(gameObject);
		instance = this;
		
		Debug.Log(instance.parametersDictionary.Count);
	}
}

[Serializable]
public struct ParameterType
{
	public TypeParam type;
	public CharacterParameter prefab;
}
