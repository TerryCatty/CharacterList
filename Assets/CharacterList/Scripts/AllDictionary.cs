using System;
using System.Collections.Generic;
using UnityEngine;

public class AllDictionary : MonoBehaviour
{
	public List<ElementType> elementsDictionary;
	public List<GroupType> groupsDictionary;
	public static AllDictionary instance;
	
	public void Awake()
	{
		if(instance != null) Destroy(gameObject);
		instance = this;
	}
}

[Serializable]
public struct ElementType
{
	public TypeElementGroup type;
	public GroupElement prefab;

}


[Serializable]
public struct GroupType
{
	public string type;
	public Group prefab;
}

