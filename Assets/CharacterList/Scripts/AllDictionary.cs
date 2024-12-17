using System;
using System.Collections.Generic;
using UnityEngine;

public class AllDictionary : MonoBehaviour
{
	public List<ElementType> elementsDictionary;
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


