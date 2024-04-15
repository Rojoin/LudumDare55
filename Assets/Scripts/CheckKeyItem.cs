using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckKeyItem : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] string deniedMessage = "Denied";
    [SerializeField] string aprobedMessage = "Aprobed";
    [SerializeField] string tagAccepted = "Player";
    [SerializeField] float showMSGTime = 3f;
    [SerializeField] private SpriteRenderer changeImage;
    [SerializeField] Sprite approvedSprite;
    public PlayerStatsSO playerStatsSO;
    public int validKey;

    public UnityEvent OnAprobed;
    public UnityEvent OnDenied;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagAccepted))
        {
            if (playerStatsSO.counterState < validKey)
            {
                // Mandar Dialogo Denided
                DialogManager.Instance.ShowDialog(deniedMessage, showMSGTime);
                OnDenied.Invoke();
            }
            else
            {
                // Mandar dialogo Aprobe
                boxCollider.enabled = false;
                if (changeImage != null)
                {
                    changeImage.sprite = approvedSprite;
                }
                DialogManager.Instance.ShowDialog(aprobedMessage, showMSGTime);
                OnAprobed.Invoke();
            }
        }
    }
}
