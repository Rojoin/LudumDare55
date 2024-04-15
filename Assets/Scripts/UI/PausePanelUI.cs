using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanelUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button MenuButton;

    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;

    private void Awake()
    {
        resumeButton.onClick.AddListener(OnResumeClicked);
        optionsButton.onClick.AddListener(OnOptionsClicked);
        MenuButton.onClick.AddListener(OnMenuClicked);
    }

    private void OnResumeClicked()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    private void OnOptionsClicked()
    {
        settingsPanel.SetActive(true);
    }
    private void OnMenuClicked()
    {
        Loader.LoadScene(Loader.Scenes.Menu);
    }
}
