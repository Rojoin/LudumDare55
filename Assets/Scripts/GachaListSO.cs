using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Create GachaListSO", fileName = "GachaListSO", order = 0)]
public class GachaListSO : ScriptableObject
{
    public string textOfBanner;
    public List<GachaCharacterSO> charactersInRotation;
    private int counterUntilLegendary = 0;
    public int MaxWishesBeforeLegendary = 3;
    public bool legendaryAlreadyDropped = false;

    public GachaCharacterSO GetLegendaryChar()
    {
        foreach (GachaCharacterSO characterSo in charactersInRotation)
        {
            if (characterSo.rarity == Rarity.keyItem)
            {
                return characterSo;
            }
        }

        return charactersInRotation[0];
    }
    

    public GachaCharacterSO GetRandomCharacterWish()
    {
        float totalProbability = 0.0f;

        foreach (GachaCharacterSO character in charactersInRotation)
        {
            totalProbability += character.chancePercentage;
        }

        float cumulative = 0.0f;

        foreach (GachaCharacterSO character in charactersInRotation)
        {
            cumulative += character.chancePercentage / totalProbability;
            character.cumulativePercentage = cumulative;
        }

        float randomNum = Random.value;

        foreach (var gachaCharacterSo in charactersInRotation)
        {
            if (randomNum <= gachaCharacterSo.cumulativePercentage)
            {
                if (gachaCharacterSo.rarity == Rarity.keyItem)
                {
                    if (!legendaryAlreadyDropped)
                    {
                        legendaryAlreadyDropped = true;
                        return gachaCharacterSo;
                    }
                    else
                    {
                        return charactersInRotation[0];
                    }
                }

                return gachaCharacterSo;
            }
        }

        return charactersInRotation[0];
    }
}