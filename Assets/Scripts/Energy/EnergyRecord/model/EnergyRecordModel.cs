using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRecordModel 
{
    public bool IsRecordActive;

    public bool IsCombatContinue;


    public double RecordStartElectricity;


    public bool ShouldShowRecord;

    public bool IsRecordShowing;


    public float timer;

    public float timerShow;

    public float MaxTimerShow { get; } = 3;
    public float timeInitialShowDelay { get; } = 2.5f;

    public float timeRecordEndAfter { get; } = 15;

    public Coroutine coroutine;

}
