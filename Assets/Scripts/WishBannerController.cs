using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WishBannerController : MonoBehaviour
{
    [SerializeField] private Image keyItemCharacterImage;
    [SerializeField] private Image[] normalCharacterImages;
    [SerializeField] private TextMeshProUGUI wishName;
    [SerializeField] private TextMeshProUGUI charName;
    [SerializeField] private TextMeshProUGUI charDescription;


    public void SetBanner(GachaListSO gachaListSo)
    {
        GachaCharacterSO legendaryChar = gachaListSo.GetLegendaryChar();
        for (int i = 0; i < normalCharacterImages.Length; i++)
        {
            normalCharacterImages[i].sprite = gachaListSo.charactersInRotation[i].image;
        }

        wishName.text = gachaListSo.textOfBanner;
        keyItemCharacterImage.sprite = legendaryChar.image;
        charName.text = legendaryChar.name;
        wishName.text = legendaryChar.description;
    }
}