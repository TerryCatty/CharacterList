using UnityEngine;

public class GroupElement : MonoBehaviour
{
   	public string nameElement {get; private set;}
	
	public ElementUI prefabUI;
	
	public ElementUI ObjectUI;
	public Group group;
	
	public void SetName(string name){
		nameElement = name;
	}

	public void DeleteElement()
	{
		group.RemoveElement(this);
		Destroy(ObjectUI.gameObject);
		Destroy(gameObject);
	}
}
