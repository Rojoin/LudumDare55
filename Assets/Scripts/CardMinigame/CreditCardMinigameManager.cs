using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditCardMinigameManager : MonoBehaviour
{
    [SerializeField] private CreditCardMinigame.CreditCard minigame;
    void Start()
    {
        minigame.onCardApproved.AddListener(StopMinigame);
    }
    public void StartMinigame()
    {
        this.gameObject.SetActive(true);
    }
    private void StopMinigame()
    {
        this.gameObject.SetActive(false);
    }
}
