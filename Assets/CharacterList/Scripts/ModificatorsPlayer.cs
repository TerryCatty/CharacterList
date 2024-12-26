using System.Linq;
using UnityEngine;

public class ModificatorsPlayer : MonoBehaviour
{
	ModificatorsList modificatorsList;
	[SerializeField] private IntParameter modificationParameter;


	private void Start(){
		modificatorsList = FindAnyObjectByType<ModificatorsList>();
	}
	
	public void SetParameter(IntParameter modificationParameter)
	{
		this.modificationParameter = modificationParameter;
	}

	public IntParameter GetParameter()
	{
		return modificationParameter;
	}

	public int GetValue()
	{
		int modificator = modificatorsList.modificators.First(m => m.valueParameter.Contains(modificationParameter.value)).valueModificator;

		return modificator;
	}
	
}
