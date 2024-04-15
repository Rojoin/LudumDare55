using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditCardEndPosFix : MonoBehaviour
{
    [SerializeField] float posY;
    private void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (CheckMobile.CheckIfMobile())
            rectTransform.position = new Vector3(rectTransform.position.x, posY);
    }
}
