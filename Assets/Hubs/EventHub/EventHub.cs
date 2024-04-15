using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHub 
{
    #region ElectricityChanged
    public static event Action<float> E_ElectricityChangedNewRate;

    public static void ElectricityChangedNewRate(float newRate)
    {
        E_ElectricityChangedNewRate?.Invoke(newRate);
    }
    #endregion

    #region PlayerMoveSpeed
    public static event Action<Vector2> E_PlayerMoveSpeed;

    public static void PlayerMoveSpeed(Vector2 playerSpeed)
    {
        E_PlayerMoveSpeed?.Invoke(playerSpeed);
    }
    #endregion
}
