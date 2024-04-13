using System.Collections;
using System.Collections.Generic;
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
    private Vector3 originalScale;

    void Start()
    {
        itemImage = item.GetComponent<Image>();
        panelImage = panel.GetComponent<Image>();
        originalScale = item.transform.localScale;


        StartCoroutine(CharacterReveal());
    }

    private IEnumerator CharacterReveal()
    {
        yield return PanelFadeIn();
        yield return ItemAnimation();
    }
    private IEnumerator PanelFadeIn()
    {
        float timer = 0;
        float startTime = Time.time;
        while (timer < panelFadeInDuration)
        {
            TimerPassTime(ref timer, ref startTime);
        }
        yield break;
    }
    private IEnumerator ItemAnimation()
    {
        SetBlackColor(itemImage);

        item.transform.localScale *= initialScaleMultiplier;
        float timer = 0;
        float prevTime = Time.time;
        Color transparent = panelImage.color - new Color(0, 0, 0, 1);
        Color semiTransparent = panelImage.color - new Color(0, 0, 0, 0.20f);
        while (timer < itemInDuration)
        {
            TimerPassTime(ref timer, ref prevTime);
            item.transform.localScale = Vector3.Lerp(item.transform.localScale, originalScale, timer / itemInDuration);
            panelImage.color = Color.Lerp(panelImage.color, transparent, timer / itemInDuration);
            yield return null;
        }

        timer = 0;
        prevTime = Time.time;

        while (timer < itemGrowDuration)
        {
            TimerPassTime(ref timer, ref prevTime);
            item.transform.localScale = Vector3.Lerp(item.transform.localScale, originalScale * finalScaleMultiplier, timer / itemGrowDuration);
            if (timer / itemGrowDuration < 0.5f)
                panelImage.color = Color.Lerp(panelImage.color, semiTransparent, timer / itemGrowDuration);
            else
                panelImage.color = Color.Lerp(panelImage.color, transparent, timer / (itemGrowDuration / 2));

            itemImage.color = Color.Lerp(Color.black, Color.white, timer / itemGrowDuration);
            yield return null;
        }
        SetWhiteColor(itemImage);
    }

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
}
