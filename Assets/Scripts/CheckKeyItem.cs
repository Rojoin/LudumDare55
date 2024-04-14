using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKeyItem : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;

    public PlayerStatsSO playerStatsSO;
    public int validKey;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerStatsSO.counterState < validKey)
            {
                // Mandar Dialogo Denided
                DialogManager.Instance.ShowDialog("No puedes pasar", 3f);
            }
            else
            {
                // Mandar dialogo Aprobe
                DialogManager.Instance.ShowDialog("Ya puedes pasar", 3f);
                boxCollider.enabled = false;
            }
        }
    }
}
