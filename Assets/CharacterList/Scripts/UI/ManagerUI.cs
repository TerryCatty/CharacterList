using TMPro;
using UnityEngine;

public class ManagerUI : MonoBehaviour
{
	private GroupsKeeper groupKeeper;
	
	[SerializeField] private GameObject canvas;
	public static ManagerUI instance;
	
	public void Awake()
	{
		if(instance != null) Destroy(gameObject);
		
		instance = this;
	}
	
	private void Start()
	{
		groupKeeper = FindAnyObjectByType<GroupsKeeper>();
	}

	
	
	public GameObject OpenWindow(GameObject window)
	{
		GameObject new_Window = Instantiate(window, canvas.transform);
		
		return new_Window;
	}
	
	public void CloseWindow(GameObject window)
	{
		Destroy(window);
	}
	
}
