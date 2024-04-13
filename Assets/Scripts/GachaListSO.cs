using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create GachaListSO", fileName = "GachaListSO", order = 0)]
public class GachaListSO : ScriptableObject
{
    public string textOfBanner;
    public List<GachaCharacterSO> charactersInRotation;
    
}