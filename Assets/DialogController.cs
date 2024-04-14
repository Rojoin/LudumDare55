using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DialogController : MonoBehaviour
{
    [SerializeField] string dialogToShow = "Sin Texto";
    [SerializeField] float timeOfDialog;

    [SerializeField] string acceptTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == acceptTag)
        {
            DialogManager.Instance.ShowDialog(dialogToShow, timeOfDialog);

            gameObject.SetActive(false);
        }
    }
}
