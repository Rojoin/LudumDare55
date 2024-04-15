using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemAnim : MonoBehaviour
{
    [SerializeField] private float panelFadeInDuration;
    [SerializeField] private float itemInDuration;
    [SerializeField] private float itemGrowDuration;
    [SerializeField] private float blackDuration;
    [SerializeField] private float initialScaleMultiplier;
    [SerializeField] private float finalScaleMultiplier;
    [SerializeField] private GameObject item;
    [SerializeField] private Image itemImage;
    [SerializeField] private GameObject panel;
    [SerializeField] private Image panelImage;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI description;
    private Sprite currentSprite;
    private Vector3 originalScale;

    void OnEnable()
    {
        
        itemImage = item.GetComponent<Image>();
        itemImage.sprite = currentSprite;
        panelImage = panel.GetComponent<Image>();
        originalScale = Vector3.one;

        StartAnim();
    }

    
    public void StartAnim()
    {
        StartCoroutine(CharacterReveal());
    }
    public void SetItem(GachaCharacterSO gacha)
    {
        currentSprite = gacha.image;
        name.text = gacha.characterName;
        description.text = gacha.description;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator CharacterReveal()
    {
        yield return PanelFadeIn();
        yield return ItemAnimation();
    }
    private IEnumerator PanelFadeIn()
    {
        item.SetActive(false);
        name.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
        item.SetActive(false);
        panel.SetActive(true);
        Color panelOpaque = panelImage.color;
        panelImage.color = Transparent(panelImage.color);
        float timer = 0;
        float startTime = Time.time;

        while (timer < panelFadeInDuration)
        {
            TimerPassTime(ref timer, ref startTime);
            panelImage.color = Color.Lerp(panelImage.color, panelOpaque, timer / itemInDuration);
            yield return null;
        }
    }
    private IEnumerator ItemAnimation()
    {
        item.SetActive(true);
   
    
        SetBlackColor(itemImage);

        item.transform.localScale *= initialScaleMultiplier;
        float timer = 0;
        float startTime = Time.time;

        while (timer < itemInDuration)
        {
            TimerPassTime(ref timer, ref startTime);
            item.transform.localScale = Vector3.Lerp(item.transform.localScale, originalScale, timer / itemInDuration);
            panelImage.color = Color.Lerp(panelImage.color, Transparent(panelImage.color), timer / itemInDuration);
            yield return null;
        }
        name.gameObject.SetActive(true);
        timer = 0;
        startTime = Time.time;
        while (timer < itemGrowDuration)
        {
            TimerPassTime(ref timer, ref startTime);
            item.transform.localScale = Vector3.Lerp(item.transform.localScale, originalScale * finalScaleMultiplier, timer / itemGrowDuration);
            if (timer / itemGrowDuration < 0.5f)
                panelImage.color = Color.Lerp(panelImage.color, SemiTransparent(panelImage.color, 0.2f), timer / itemGrowDuration);
            else
                panelImage.color = Color.Lerp(panelImage.color, Transparent(panelImage.color), timer / (itemGrowDuration / 2));

            itemImage.color = Color.Lerp(Color.black, Color.white, timer / itemGrowDuration);
            yield return null;
        }
        description.gameObject.SetActive(true);
        SetWhiteColor(itemImage);
    }

    #region Utils
    private static void TimerPassTime(ref float timer, ref float prevTime)
    {
        timer += Time.time - prevTime;
        prevTime = Time.time;
    }
    private void SetBlackColor(Image image)
    {
        image.color = Color.black;
    }
    private void SetWhiteColor(Image image)
    {
        image.color = Color.white;
    }
    private Color Transparent(Color original)
    {
        return original - new Color(0, 0, 0, original.a);
    }
    private Color SemiTransparent(Color original, float amount)
    {
        return Transparent(original) + new Color(0, 0, 0, amount);
    }
    private Color Opaque(Color original)
    {
        return original + new Color(0, 0, 0, 1f - original.a);
    }
    #endregion
}
