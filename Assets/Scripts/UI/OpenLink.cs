using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public void OpenLinkPage(string url)
    {
        Application.OpenURL(url);
    }
}