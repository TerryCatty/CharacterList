using TMPro;
using UnityEngine;

public class ConfirmWindow : MonoBehaviour
{
    [SerializeField] private IConfirmable confirmObject;
    [SerializeField] private TextMeshProUGUI message;

    public void SetText(string text)
    {
       message.text = text;
    }
    public void SetConfirm(IConfirmable confirmObject)
    {
        this.confirmObject = confirmObject;
    }
    public void Confirm()
    {
        confirmObject.Confirm();
        Destroy(gameObject);
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}