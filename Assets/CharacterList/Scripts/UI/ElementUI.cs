using UnityEngine;

public class ElementUI : MonoBehaviour
{
	protected string nameElement;
	
	
	public virtual void SetName(string name)
	{
		nameElement = name;
	}
	
	public virtual void Init(){}
}
