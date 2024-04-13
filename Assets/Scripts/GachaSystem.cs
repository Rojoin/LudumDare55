using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GachaSystem : MonoBehaviour
{
    public List<GachaListSO> posiblesGachaList;
    public List<GachaCharacterDisplay> gachaDisplay;
    private GachaListSO currentGachaList;
    private List<GachaCharacterSO> currentWishes = new List<GachaCharacterSO>();
    [SerializeField] private int wishesPerRoll;
    public UnityEvent onLegendaryDropped;

    void Start()
    {
        wishesPerRoll = gachaDisplay.Count;
        currentGachaList = posiblesGachaList[0];
        GetCharactersList();
        for (int i = 0; i < wishesPerRoll; i++)
        {
            gachaDisplay[i].SetGachaCharacter(currentWishes[i]);
        }
    }


    void Update()
    {
    }

    void GetCharactersList()
    {
        currentWishes.Clear();
        for (int i = 0; i < wishesPerRoll; i++)
        {
            currentWishes.Add(currentGachaList.getRandomCharacter());
            if (currentGachaList.legendaryAlreadyDropped)
            {
                onLegendaryDropped.Invoke();
            }
        }
    }
}

public enum Rarity
{
    normal = 1,
    keyItem = 1
}