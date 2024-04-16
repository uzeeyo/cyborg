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

    #region CombatStarted
    public static event Action E_CombatStarted;

    public static void CombatStarted()
    {
        E_CombatStarted?.Invoke();
    }
    #endregion

    #region CombatEnd
    public static event Action E_CombatEnd;

    public static void CombatEnd()
    {
        E_CombatEnd?.Invoke();
    }
    #endregion

    #region InventoryChanged
    public static event Action E_InventoryChanged;

    public static void InventoryChanged()
    {
        E_InventoryChanged?.Invoke();
    }
    #endregion

    #region EnergyAbsorbed
    public static event Action E_EnergyAbsorbed;

    public static void EnergyAbsorbed()
    {
        E_EnergyAbsorbed?.Invoke();
    }
    #endregion

    #region EnergyEnded
    public static event Action E_EnergyEnded;

    public static void EnergyEnded()
    {
        E_EnergyEnded?.Invoke();
    }
    #endregion

    #region EnergyEnded
    public static event Action E_PlayerDeath;

    public static void PlayerDeath()
    {
        E_PlayerDeath?.Invoke();
    }
    #endregion


}
