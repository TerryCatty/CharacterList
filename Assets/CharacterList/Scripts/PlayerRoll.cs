  using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    [SerializeField] private int value;

    ModificatorsPlayer modificatorsPlayer;

    private void Start(){
        modificatorsPlayer = GetComponent<ModificatorsPlayer>();
    }

    public void Roll()
    {
        CheckResult();
    }

    public void CheckResult()
    {
        Debug.Log(modificatorsPlayer.GetValue());
    }
}
