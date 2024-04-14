using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviourSingleton<DialogManager>
{
    [SerializeField] TextMeshProUGUI _dialogUGUI;
    [SerializeField] float dialogTime;

    [SerializeField] float fadeTime;

    [SerializeField] List<string> dialogsQueue;

    bool dialogVisible = false;

    private void Update()
    {
        if (_dialogUGUI.alpha < 1.0f)
            FadeIn();
    }

    void FadeIn()
    {
        float alpha = 0f;

        alpha = Mathf.Lerp(alpha, 1, fadeTime * Time.deltaTime);
        _dialogUGUI.alpha = alpha;

        Debug.LogWarning(alpha);

        Debug.Log(alpha);
    }

    void FadeOut()
    {
        float alpha = 1f;

        while (alpha > 0) 
        {
            alpha = Mathf.Lerp(alpha, 0, fadeTime * Time.deltaTime);
            _dialogUGUI.alpha = alpha;

            Debug.LogWarning(alpha);
        }

        Debug.Log(alpha);
    }

    void QueueDialog()
    {
        if (dialogsQueue.First() != null)
            StartCoroutine(CountDownShowDialog(dialogsQueue.First(), dialogTime));
    }

    public void ShowDialog(string dialogText, float time)
    {
        if (!dialogVisible) 
        {
            dialogVisible = true;
            StartCoroutine(CountDownShowDialog(dialogText, time));
        }
        else dialogsQueue.Add(dialogText);
    }

    IEnumerator CountDownShowDialog(string dialogText, float time)
    {
        _dialogUGUI.text = dialogText;

        yield return new WaitForSeconds(time);

        _dialogUGUI.text = "";
        dialogVisible = false;
    }
}

