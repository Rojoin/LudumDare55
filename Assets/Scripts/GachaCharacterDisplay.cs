using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaCharacterDisplay : MonoBehaviour
{
    public GachaCharacterSO currentChar;
    public Image _image;
    
    public void SetGachaCharacter(GachaCharacterSO gacha)
    {
        currentChar = gacha;
        SetCharacterImage();
    }
    private void SetCharacterImage()
    {
        _image.sprite = currentChar.image;
    }
    
}
