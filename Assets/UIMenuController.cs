using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    public CanvasGroup mainMenu;
    public CanvasGroup options;
    public CanvasGroup credits;

    void Start()
    {
        SetCanvasState(mainMenu, true);
        SetCanvasState(options, false);
        SetCanvasState(credits, false);
    }

    void Update()
    {
        
    }

    void SetCanvasState(CanvasGroup canvas, bool state)
    {
        canvas.alpha = state ? 1.0f : 0.0f;
        canvas.interactable = state;
        canvas.blocksRaycasts = state;
    }

    public void ChangeToOptions()
    {
        SetCanvasState(mainMenu, false);
        SetCanvasState (options, true);
    }

    public void ChangeToCredits()
    {
        SetCanvasState(mainMenu, false);
        SetCanvasState(credits, true);
    }

    public void GoBackToMenu()
    {
        SetCanvasState(mainMenu, true);
        SetCanvasState(options, false);
        SetCanvasState(credits, false);
    }
}
