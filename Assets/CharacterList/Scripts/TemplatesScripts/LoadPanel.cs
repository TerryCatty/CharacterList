using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;

public class LoadPanel : MonoBehaviour
{
	[SerializeField] private TMP_Dropdown choosingSave;
	
	[SerializeField] private List<string> options;

    private void Start()
    {
		Init();
    }
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


	public void ClosePanel()
	{
		ManagerUI.instance.CloseWindow(gameObject);
	}

	public void LoadSave()
	{
		if (choosingSave.options[choosingSave.value].text == SaveManager.instance.getStart) return;

		if(choosingSave.options[choosingSave.value].text.Contains(PatternSystem.instance.templatePath) == false)
            SaveManager.instance.SetStartFolder(choosingSave.options[choosingSave.value].text);

        SaveManager.instance.StartLoad(false);
	}
}
