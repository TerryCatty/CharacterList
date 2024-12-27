using UnityEngine;

public class PatternSystem : MonoBehaviour
{
	[SerializeField] private string templateFolder = "Template";
   [SerializeField] private string nameTemplate;
   
   [SerializeField] private GroupsKeeper groupsKeeper;
   [SerializeField] private LoadPanel loadPanel;
   
   
   public static PatternSystem instance;
   
   private void Awake()
   {
   	instance = this;
   }
   
   public void OpenCreatePanel()
   {
   		ManagerUI.instance.OpenWindow(loadPanel.gameObject);
   }
   
   public void AddTemplate(string nameTemplate)
   {
   	this.nameTemplate = nameTemplate;
	SaveTemplate();
   }
   
   public void SaveTemplate()
   {
		string start = SaveManager.instance.getStart;
		SaveManager.instance.SetStartFolder(templateFolder + "/" + nameTemplate);
		SaveManager.instance.SaveAll();
		SaveManager.instance.SetStartFolder(start);
   }
   
   public void LoadTemplate()
   {
		string start = SaveManager.instance.getStart;
		SaveManager.instance.SetStartFolder(templateFolder + "/" + nameTemplate);
		groupsKeeper.StartLoad(false);
		SaveManager.instance.SetStartFolder(start);
   }
}
