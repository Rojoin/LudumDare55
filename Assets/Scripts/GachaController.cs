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
    public Animator animator;
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
    [SerializeField] private CanvasGroup container;

    [Header("Variables")]
    private int wishCounter = 0;
    private int maxWishesPerRound;
    public List<GachaListSO> posiblesGachaList;
    public List<GachaCharacterDisplay> gachaDisplay;
    private GachaListSO currentGachaList;
    private List<GachaCharacterSO> currentWishes = new List<GachaCharacterSO>();
    private int currentList = 0;
    [SerializeField] private TextMeshProUGUI textMesh;
    [Header("Events")]
    public UnityEvent onLegendaryDropped;
    public UnityEvent onStoryContinue;
    public UnityEvent onLegendarySound;
    public UnityEvent onMessiSound;
    public UnityEvent onNormalSound;
    public UnityEvent onSummonSound;
    private static readonly int Summon = Animator.StringToHash("Summon");
    private bool firstTimeLegendary = false;

    private void OnEnable()
    {
       
        container.SetCanvasState(true);
        currentList = playerStats.counterState;
        currentGachaList = posiblesGachaList[currentList];
        wishButton.onClick.AddListener(TryWishSummon);
        buyButton.onClick.AddListener(ActivateCreditCardGame);
        goBackToWishes.onClick.AddListener(HideBuyPrompt);
        nextScreen.onClick.AddListener(TryNextCharacter);
        goBackToWishesAfterGacha.onClick.AddListener(GoBackToWishScreen);
        onLegendaryDropped.AddListener(LegendaryDropped);
        _wishBannerController.SetBanner(currentGachaList);
        firstTimeLegendary = false;
        SetCanvasState(wishBuyScreen, true);
        SetCanvasState(buyPromptScreen, false);
        SetCanvasState(showGachaSummon, false);
        SetCanvasState(showAllSummons, false);
        SetCanvasState(showCreditCard, false);
    }

    private void LegendaryDropped()
    {
        playerStats.counterState++;
        firstTimeLegendary = true;
    }

    private void OnDisable()
    {
        container.SetCanvasState(false);
        wishButton.onClick.RemoveListener(TryWishSummon);
        buyButton.onClick.RemoveListener(ActivateCreditCardGame);
        goBackToWishes.onClick.RemoveListener(HideBuyPrompt);
        nextScreen.onClick.RemoveListener(TryNextCharacter);
        goBackToWishesAfterGacha.onClick.RemoveListener(GoBackToWishScreen);
        onLegendaryDropped.RemoveListener(LegendaryDropped);
    }

    private void TryWishSummon()
    {
        if (playerStats.money >= 5)
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
        SetCanvasState(showCreditCard, true);
        creditCardGame.enabled = true;
        creditCardGame.onCardApproved.AddListener(DeactivateCreditCard);
    }

    private void DeactivateCreditCard()
    {
        playerStats.money += 5;
        textMesh.text = "Wishes:" + playerStats.money;
        creditCardGame.enabled = false;
        SetCanvasState(showCreditCard, false);
        creditCardGame.onCardApproved.RemoveListener(DeactivateCreditCard);
    }

    private void SummonCharacter()
    {
        SetCanvasState(wishBuyScreen, false);
        onSummonSound.Invoke();
        animator.SetTrigger(Summon);
        Invoke(nameof(ChangeToCharacterShowCase), 0.8f);
    }

    private void ChangeToCharacterShowCase()
    {
        GetCharactersList();
        SetCanvasState(showGachaSummon, true);
        wishCounter = 0;
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
        if (wishCounter < maxWishesPerRound)
        {
            animator.SetTrigger(Summon);
            gachaDisplay[wishCounter].SetGachaCharacter(currentWishes[wishCounter]);
            if (currentWishes[wishCounter].rarity == Rarity.keyItem)
            {
                onLegendarySound.Invoke();
                if (currentWishes[wishCounter].characterName == "Ancarameci")
                {
                    onMessiSound.Invoke();
                }
            }
            else
            {
                onNormalSound.Invoke();
            }

            item.SetItem(currentWishes[wishCounter]);
            item.enabled = true;
            item.StartAnim();
            wishCounter++;
        }
        else
        {
            ShowAllCharacters();
        }
    }

    private void ShowAllCharacters()
    {
        SetCanvasState(showGachaSummon, false);
        SetCanvasState(showAllSummons, true);
    }

    private void GoBackToWishScreen()
    {
        SetCanvasState(showAllSummons, false);
        SetCanvasState(wishBuyScreen, true);
        if (currentList != playerStats.counterState)
        {
            onStoryContinue.Invoke();
        }
    }


    void GetCharactersList()
    {
        currentWishes.Clear();
        for (int i = 0; i < gachaDisplay.Count; i++)
        {
            currentWishes.Add(currentGachaList.GetRandomCharacterWish());
            if (currentGachaList.legendaryAlreadyDropped && !firstTimeLegendary)
            {
                onLegendaryDropped.Invoke();
            }
        }

        currentGachaList.counterUntilLegendary++;
    }
}

public enum Rarity
{
    normal = 10,
    keyItem = 2
}