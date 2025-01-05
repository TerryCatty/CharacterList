using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RollWindow : MonoBehaviour
{
    [SerializeField] private PlayerRoll playerRoll;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private Toggle applyBuffs;

    public void SetText(string text)
    {
        message.text = text;
    }
    public void SetPlayerRoll(PlayerRoll playerRoll)
    {
        this.playerRoll = playerRoll;
    }
    public void Reroll()
    {
        playerRoll.Roll();
        Destroy(gameObject);
    }

    public void Confirm()
    {
        playerRoll.ConfirmRoll(applyBuffs.isOn);
        Destroy(gameObject);
    }
}
