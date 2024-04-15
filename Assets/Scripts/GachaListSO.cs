
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(menuName = "Create GachaListSO", fileName = "GachaListSO", order = 0)]
public class GachaListSO : ScriptableObject
{
    public string textOfBanner;
    public List<GachaCharacterSO> charactersInRotation;
    public int counterUntilLegendary = 0;
    public int MaxWishesBeforeLegendary = 3;
    public bool legendaryAlreadyDropped = false;
    private List<GachaCharacterSO> common = new List<GachaCharacterSO>();

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
private GachaCharacterSO GetRandomCommonCharacterSO()
{
    foreach (GachaCharacterSO characterSo in charactersInRotation)
    {
        if (characterSo.rarity != Rarity.keyItem)
        {
            common.Add(characterSo);
        }
    }

    int random = Random.Range(0,common.Count);

    return common[random];
}
    private void OnDisable()
    {
        counterUntilLegendary = 0;
        legendaryAlreadyDropped = false;
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
            character.cumulativePercentage = character.chancePercentage / totalProbability;
        }

        float randomNum;
        if (counterUntilLegendary >= MaxWishesBeforeLegendary && !legendaryAlreadyDropped)
        {
            randomNum = 1;
            Debug.Log("Secure Legendary");
        }
        else
        {
            randomNum = Random.value;
        }

        Debug.Log(randomNum);

        if (Math.Abs(randomNum - 1) < 0.0001f & !legendaryAlreadyDropped)
        {
            legendaryAlreadyDropped = true;
            return GetLegendaryChar();
        }

        return GetRandomCommonCharacterSO();
    }
}