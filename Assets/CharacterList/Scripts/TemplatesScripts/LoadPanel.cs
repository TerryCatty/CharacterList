using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;

public class LoadPanel : MonoBehaviour
{
	[SerializeField] private TMP_Dropdown choosingSave;
	
	[SerializeField] private List<string> options;
	public void Init()
	{
		choosingSave.ClearOptions();
		options = new List<string>();
		
		foreach(SavePath save in SaveManager.instance.savePaths)
		{
			options.Add(save.name);
		}
		
		choosingSave.AddOptions(options);
	}
}
