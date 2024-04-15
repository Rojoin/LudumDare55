using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GachaController _gachaController;

    [SerializeField] private CanvasGroup pauseMenu;
    [SerializeField] private Button pauseMenuButton;
    [SerializeField] private Button gachaPonButton;
    [SerializeField] private Button goBackToGameplayFromGacha;
    [SerializeField] private Button goBackToGameplayFromPause;
    [SerializeField] private Button goBackToMenu;
    [SerializeField] private PlayerController player;
    private bool isPauseOn = false;
    private bool isGachaOn = false;

    private void OnEnable()
    {
        _gachaController = GetComponent<GachaController>();
        _gachaController.enabled = isGachaOn;
        pauseMenu.SetCanvasState(isPauseOn);
        pauseMenuButton.onClick.AddListener(TogglePause);
        gachaPonButton.onClick.AddListener(ToggleGacha);
        goBackToGameplayFromGacha.onClick.AddListener(ToggleGacha);
        goBackToGameplayFromPause.onClick.AddListener(TogglePause);
        goBackToMenu.onClick.AddListener(ReturnToMenu);
        _gachaController.onStoryContinue.AddListener(ToggleGacha);
    }

    private void OnDisable()
    {
        pauseMenuButton.onClick.RemoveAllListeners();
        gachaPonButton.onClick.RemoveAllListeners();
        goBackToGameplayFromGacha.onClick.RemoveAllListeners();
        goBackToGameplayFromPause.onClick.RemoveAllListeners();
        goBackToMenu.onClick.RemoveAllListeners();
        _gachaController.onStoryContinue.RemoveAllListeners();
    }

    private void TogglePause()
    {
        isPauseOn = !isPauseOn;
        pauseMenu.SetCanvasState(isPauseOn);
        player.enabled = !isPauseOn;
    }

    private void ToggleGacha()
    {
        isGachaOn = !isGachaOn;
        _gachaController.enabled = isGachaOn;
        player.enabled = !isGachaOn;
        
    }

    private void ReturnToMenu()
    {
        Loader.LoadScene(Loader.Scenes.Menu);
    }
    
    public void EndGame()
    {
        Invoke(nameof(ReturnToMenu),5.0f);
    }
}

public static class CanvasController

{
    public static void SetCanvasState(this CanvasGroup canvas, bool state)
    {
        canvas.alpha = state ? 1.0f : 0.0f;
        canvas.interactable = state;
        canvas.blocksRaycasts = state;
    }
}