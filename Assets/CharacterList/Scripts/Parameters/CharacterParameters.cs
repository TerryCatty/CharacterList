using System;
using System.Collections.Generic;
using UnityEngine;


public class CharacterParameters : CharacterPart{
   public List<CharacterParameter> parameters;

   public string nameParameter;
   public TypeElementGroup typeParam;


   

   // }

   // private void CreateParameter<T>() where T : CharacterParameter
   // {

   //    T newParameter = new GameObject("Parameter").AddComponent<T>();
   //    newParameter.SetName(nameParameter);

   //    parameters.Add(newParameter);
   // }
}