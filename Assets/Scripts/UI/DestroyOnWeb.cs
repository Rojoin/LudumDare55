using UnityEngine;

public class DestroyOnWeb : MonoBehaviour
{
    private void Awake()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Destroy(this.gameObject);
        }
    }
}