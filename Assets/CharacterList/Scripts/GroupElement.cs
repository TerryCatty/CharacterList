using UnityEngine;

public class GroupElement : MonoBehaviour
{
   	public string nameElement {get; private set;}
	
	public void SetName(string name){
		nameElement = name;
	}

}
