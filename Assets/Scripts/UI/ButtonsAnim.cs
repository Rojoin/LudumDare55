using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonsAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Action onButtonEnter;
    public Action onButtonExit;

    [Header("Effect Scale:")]
    [SerializeField] private float scaleSpeed = 3;
    [SerializeField] private float scaleLimit = 1.2f;
    private bool increment = false;
    private Vector3 initialScale;
    private Vector3 scale;

    [Header("Effect Color:")]
    [SerializeField] private bool modifyColor;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Color clickedHighlightColor;
    private Color originalColor;

    [Header("Effect Image:")]
    [SerializeField] private bool modifyImage;
    [SerializeField] private bool baseImageIsTransparent;

    [SerializeField] private Sprite imageDefault;
    [SerializeField] private Sprite imageHighlighted;
    [SerializeField] private Image currentImage;

    [Header("Effect Color Text:")]
    [SerializeField] private bool textHighlight;

    [SerializeField] private TextMeshProUGUI textToHighlight;
    [SerializeField] private Color textColorHighlight;

    private void Awake()
    {
        increment = false;
        initialScale = transform.localScale;

        if (modifyImage)
            if (!currentImage)
                currentImage = GetComponent<Image>();

        //if (enableObject)
        //{
        //    if (!objectToEnable)
        //    {

        //        enableObject = false;
        //    }

        //}
        if(modifyColor)
        {
            originalColor = buttonImage.color;
        }

        OnMouseExitButton();
    }
    private void OnEnable()
    {
        transform.localScale = initialScale;
        increment = false;
    }

    private void Update()
    {
        ChangeScale();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExitButton();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnterButton();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(modifyColor)
        {
            buttonImage.color = clickedHighlightColor;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (modifyColor)
        {
            buttonImage.color = originalColor;
        }
    }
    public void OnMouseEnterButton()
    {
        onButtonEnter?.Invoke();
        increment = true;

        if (modifyImage)
        {
            currentImage.sprite = imageHighlighted;
            currentImage.color = Color.white;
        }

        //if (enableObject)
        //    objectToEnable.SetActive(true);

        if (textHighlight)
            textToHighlight.color = textColorHighlight;
    }

    public void OnMouseExitButton()
    {
        onButtonExit?.Invoke();
        increment = false;

        if (modifyImage)
        {
            currentImage.sprite = imageDefault;
            if (baseImageIsTransparent)
            {
                currentImage.color = Color.clear;
            }
        }

        //if (enableObject)
        //    objectToEnable.SetActive(false);

        //if (textHighlight)
        //    textToHighlight.color = colorNormal;
    }

    private void ChangeScale()
    {
        float timeStep = scaleSpeed * Time.unscaledDeltaTime;
        scale = transform.localScale;
        if (increment)
        {
            if (transform.localScale.x < scaleLimit)
            {
                scale = new Vector3(scale.x + timeStep, scale.y + timeStep, scale.z + timeStep);
                transform.localScale = scale;
            }
            else
            {
                transform.localScale = new Vector3(scaleLimit, scaleLimit, scaleLimit);
            }
        }
        else
        {
            if (transform.localScale.x > initialScale.x)
            {
                scale = new Vector3(scale.x - timeStep, scale.y - timeStep, scale.z - timeStep);
                transform.localScale = scale;
            }
            else
            {
                transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
            }
        }
    }

}
