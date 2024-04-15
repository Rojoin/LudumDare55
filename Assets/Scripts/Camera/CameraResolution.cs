using Cinemachine;
using UnityEngine;
public class CameraResolution : MonoBehaviour
{
    public int fullWidthUnits = 14;

    void Start()
    {
        if ((CheckMobile.CheckIfMobile()))
        {
            float ratio = (float)Screen.height / (float)Screen.width;
            GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = (float)fullWidthUnits * ratio / 2.0f;
        }
    }
}
