using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileButtons : MonoBehaviour
{
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;

#if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern bool IsMobile();
#endif

    private void Awake()
    {
        if (!CheckMobile.CheckIfMobile())
        {
            Destroy(leftButton);
            Destroy(rightButton);
        }
    }

//    private bool CheckIfMobile()
//    {
//        bool isMobile = false;

//#if !UNITY_EDITOR && UNITY_WEBGL
//        isMobile = IsMobile();
//#endif

//        return isMobile;
//    }
}
