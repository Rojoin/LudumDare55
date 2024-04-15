using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerStatsSO", fileName = "PlayerStatsSO", order = 0)]
public class PlayerStatsSO : ScriptableObject
{
    public int counterState = 0;
    public int money = 5;

    private void OnDisable()
    {
        OnReset();
    }

    public void OnReset()
    {
        counterState = 0;
        money = 5;
    }
}