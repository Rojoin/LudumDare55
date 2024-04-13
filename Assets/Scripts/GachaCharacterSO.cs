using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Create GachaCharacterSO", fileName = "GachaCharacterSO", order = 0)]
public class GachaCharacterSO : ScriptableObject
{
    public string characterName;
    public string description;
    public Sprite image;
    public Rarity rarity;
    public float cumulativePercentage;
    public int chancePercentage
    {
        get
        {
            return (int)rarity;
        }
    }
}