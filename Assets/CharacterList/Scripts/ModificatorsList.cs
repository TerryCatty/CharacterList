using System;
using System.Collections.Generic;
using UnityEngine;

public class ModificatorsList : MonoBehaviour
{
    public List<Modificator> modificators;

}

[Serializable]
public struct Modificator{
    public List<int> valueParameter;
    public int valueModificator;
}