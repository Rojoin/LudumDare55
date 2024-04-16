using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button quitButton;

    [SerializeField] GameObject soundManager;
    [SerializeField] GameObject optionsPanel;
   
    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(soundManager);
        SoundManager.Instance.PlayMusicGame();
        playButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scenes.Game);
        });

        optionsButton.onClick.AddListener(() =>
        {
            optionsPanel.SetActive(true);
        });

        quitButton.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
#endif
            Application.Quit();
        });
        
    }
}
