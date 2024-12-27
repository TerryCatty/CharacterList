using TMPro;
using UnityEngine;

public class TemplateCreatePanel : MonoBehaviour
{
	[SerializeField] private TMP_InputField inputName;
  public void CreateTemplate()
  {
	PatternSystem.instance.AddTemplate(inputName.text);
  }
  
  public void CloseWindow()
  {
  	ManagerUI.instance.CloseWindow(gameObject);
  }
}
