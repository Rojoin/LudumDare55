using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MovementButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerController player;
    [SerializeField] private int movementValue;
    public void OnPointerDown(PointerEventData eventData)
    {
        player.ButtonInput(movementValue);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.ButtonInput(0);
    }
}
