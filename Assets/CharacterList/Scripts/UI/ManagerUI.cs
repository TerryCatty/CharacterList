using TMPro;
using UnityEngine;

public interface IConfirmable
{
	public void Confirm();
	public void Cancel();
}

public class ManagerUI : MonoBehaviour
{
	private GroupsKeeper groupKeeper;
	
	[SerializeField] private GameObject canvas;
	public static ManagerUI instance;

	[SerializeField] private ConfirmWindow confirmPanelPrefab;

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

		Debug.Log("open");
		
		return new_Window;
	}

	public void ConfirmWindow(string text, IConfirmable confirmObj)
	{
        ConfirmWindow confirm = OpenWindow(confirmPanelPrefab.gameObject).GetComponent<ConfirmWindow>();
		confirm.SetConfirm(confirmObj);
		confirm.SetText(text);

    }
	
	public void CloseWindow(GameObject window)
	{
		Destroy(window);
	}
	
}
