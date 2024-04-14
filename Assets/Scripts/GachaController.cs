using System;
using System.Collections.Generic;
using CreditCardMinigame;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GachaController : MonoBehaviour
{
    [Header("Classes")]
    public PlayerStatsSO playerStats;
    public ItemAnim item;
    public CreditCard creditCardGame;
    [SerializeField] private WishBannerController _wishBannerController;
    [Header("Buttons")]
    public Button wishButton;
    public Button buyButton;
    public Button goBackToWishes;
    public Button nextScreen;
    public Button goBackToWishesAfterGacha;

    [Header("Canvas")]
    [SerializeField] private CanvasGroup wishBuyScreen;
    [SerializeField] private CanvasGroup buyPromptScreen;
    [SerializeField] private CanvasGroup showGachaSummon;
    [SerializeField] private CanvasGroup showAllSummons;
    [SerializeField] private CanvasGroup showCreditCard;

    [Header("Variables")]
    private int counter = 0;
    private int maxWishesPerRound;
    public List<GachaListSO> posiblesGachaList;
    public List<GachaCharacterDisplay> gachaDisplay;
    private GachaListSO currentGachaList;
    private List<GachaCharacterSO> currentWishes = new List<GachaCharacterSO>();
    [SerializeField]  private TextMeshProUGUI textMesh;
    [Header("Events")]
    public UnityEvent onLegendaryDropped;

    private void OnEnable()
    {
        currentGachaList = posiblesGachaList[playerStats.counterState];
        wishButton.onClick.AddListener(TryWishSummon);
        buyButton.onClick.AddListener(ActivateCreditCardGame);
        goBackToWishes.onClick.AddListener(HideBuyPrompt);
        nextScreen.onClick.AddListener(TryNextCharacter);
        goBackToWishesAfterGacha.onClick.AddListener(GoBackToWishScreen);
        _wishBannerController.SetBanner(currentGachaList);
        SetCanvasState(wishBuyScreen, true);
        SetCanvasState(buyPromptScreen, false);
        SetCanvasState(showGachaSummon, false);
        SetCanvasState(showAllSummons, false);
        SetCanvasState(showCreditCard, false);
    }

    private void OnDisable()
    {
        wishButton.onClick.RemoveListener(TryWishSummon);
        buyButton.onClick.RemoveListener(ActivateCreditCardGame);
        goBackToWishes.onClick.RemoveListener(HideBuyPrompt);
        nextScreen.onClick.RemoveListener(TryNextCharacter);
        goBackToWishesAfterGacha.onClick.RemoveListener(GoBackToWishScreen);
    }

    private void TryWishSummon()
    {
        if (playerStats.money == 5)
        {
            playerStats.money -= 5;
            textMesh.text = "Wishes:" + playerStats.money;
            SummonCharacter();
        }
        else
        {
            ShowBuyPrompt();
        }
    }

    private void ShowBuyPrompt()
    {
        SetCanvasState(buyPromptScreen, true);
    }

    private void HideBuyPrompt()
    {
        SetCanvasState(buyPromptScreen, false);
    }
    private void ActivateCreditCardGame()
    {
        HideBuyPrompt();
        SetCanvasState(showCreditCard,true);
        creditCardGame.enabled = true;
        creditCardGame.onCardApproved.AddListener(DeactivateCreditCard);
    }
    private void DeactivateCreditCard()
    {
        playerStats.money += 5;
        textMesh.text = "Wishes:" + playerStats.money;
        creditCardGame.enabled = false;
        SetCanvasState(showCreditCard,false);
        creditCardGame.onCardApproved.RemoveListener(DeactivateCreditCard);
    }

    private void SummonCharacter()
    {
        GetCharactersList();
        SetCanvasState(showGachaSummon,true);
        counter = 0;
        TryNextCharacter();
    }

    void SetCanvasState(CanvasGroup canvas, bool state)
    {
        canvas.alpha = state ? 1.0f : 0.0f;
        canvas.interactable = state;
        canvas.blocksRaycasts = state;
    }

    private void TryNextCharacter()
    {
        item.enabled = false;

         maxWishesPerRound = gachaDisplay.Count;
        if (counter < maxWishesPerRound)
        {
            gachaDisplay[counter].SetGachaCharacter(currentWishes[counter]);
            item.SetSprite(currentWishes[counter].image);
            item.enabled = true;
            item.StartAnim();
            counter++;
        }
        else
        {
            ShowAllCharacters();
        }
    }

    private void ShowAllCharacters()
    {
        SetCanvasState(showGachaSummon,false);
        SetCanvasState(showAllSummons,true);
    }
    private void GoBackToWishScreen ()
    {
        SetCanvasState(showAllSummons,false);
    }


    void GetCharactersList()
    {
        currentWishes.Clear();
        for (int i = 0; i < gachaDisplay.Count; i++)
        {
            currentWishes.Add(currentGachaList.GetRandomCharacterWish());
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
    keyItem = 2
}