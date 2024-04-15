using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviourSingleton<DialogManager>
{
    [SerializeField] TextMeshProUGUI _dialogUGUI;
    [SerializeField] List<string> dialogsQueue;
    [SerializeField] List<float> dialogsQueueTime;

    [SerializeField] float fadeVelocity = 3f;

    bool dialogInUse = false;

    private void Update()
    {
        if (_dialogUGUI.alpha < 1f && dialogInUse)
            FadeIn();
        else if (_dialogUGUI.alpha > 0f && !dialogInUse)
            FadeOut();

        if (dialogsQueue.Count > 0)
            StartCoroutine(VerifyDialogQueue());
    }

    void FadeIn()
    {
        _dialogUGUI.alpha = Mathf.Lerp(_dialogUGUI.alpha, 1, fadeVelocity * Time.deltaTime);

        _dialogUGUI.alpha = (_dialogUGUI.alpha > 0.9) ? 1 : _dialogUGUI.alpha;
    }
    void FadeOut()
    {
        _dialogUGUI.alpha = Mathf.Lerp(_dialogUGUI.alpha, 0, fadeVelocity * Time.deltaTime);

        _dialogUGUI.alpha = (_dialogUGUI.alpha < 0.1) ? 0 : _dialogUGUI.alpha;
    }
    void QueueDialog()
    {
        if (dialogsQueue.Count > 0 && !dialogInUse)
        {
            dialogInUse = true;
            StartCoroutine(CountDownShowDialog(dialogsQueue.First(), dialogsQueueTime.First()));

            Debug.Log($"Dialog Start | String: {dialogsQueue.First()}");
            Debug.Log($"Dialog Start | Time: {dialogsQueueTime.First()}");

            dialogsQueue.Remove(dialogsQueue.First());
            dialogsQueueTime.Remove(dialogsQueueTime.First());
        }
    }

    public void ShowDialog(string dialogText, float time)
    {
        if (!dialogInUse)
        {
            dialogInUse = true;
            StartCoroutine(CountDownShowDialog(dialogText, time));
        }
        else
        {
            dialogsQueue.Add(dialogText);
            dialogsQueueTime.Add(time);
        }
    }

    IEnumerator CountDownShowDialog(string dialogText, float time)
    {
        _dialogUGUI.alpha = 0;
        _dialogUGUI.text = dialogText;

        yield return new WaitForSeconds(time);

        dialogInUse = false;

        Debug.Log("Termino la coorrutina");
    }
    IEnumerator VerifyDialogQueue()
    {
        QueueDialog();

        if (dialogsQueue.Count > 0)
        {
            yield return new WaitForSeconds(3f);

            StartCoroutine(VerifyDialogQueue());
        }
    }
}

