using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModificatorsPlayer : MonoBehaviour
{
	ModificatorsList modificatorsList;
	[SerializeField] private IntParameter modificationParameter;
	public int bonusValue;



	private void Start(){
		modificatorsList = FindAnyObjectByType<ModificatorsList>();
	}
	
	public void SetParameter(IntParameter modificationParameter)
	{
		this.modificationParameter = modificationParameter;
	}


	public int GetValue()
	{
		int modificator = modificatorsList.modificators.First(m => m.valueParameter.Contains(modificationParameter.value)).valueModificator;

		modificator += bonusValue;

		return modificator;
	}
}
