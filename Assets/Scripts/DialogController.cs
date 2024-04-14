using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DialogController : MonoBehaviour
{
    [SerializeField] string dialogToShow = "Sin Texto";
    [SerializeField] string acceptTag = "Player";

    [SerializeField] float timeOfDialog = 3f;
    [SerializeField] float repeatEvery = 10f;
    float timer;

    [SerializeField] bool repeat = false;

    private void Update()
    {
        timer = (timer <= 0 && repeat) ?  0 : timer - Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == acceptTag && timer <= 0)
        {
            DialogManager.Instance.ShowDialog(dialogToShow, timeOfDialog);
            timer = repeatEvery;
        }
    }
}
