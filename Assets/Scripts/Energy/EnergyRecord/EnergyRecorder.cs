using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRecorder : MonoBehaviour
{
    public static EnergyRecorder Instance { get; private set; }

    private EnergyRecordModel model = new EnergyRecordModel();
    private void Awake()
    {
        Instance = this;

        EventHub.E_CombatStarted += CombatStart;
        EventHub.E_CombatEnd += CombatEnd;
        EventHub.E_InventoryChanged += ResetRecordTimer;
        EventHub.E_EnergyAbsorbed += ResetRecordTimer;

    }
    private void OnDestroy()
    {
        EventHub.E_CombatStarted -= CombatStart;
        EventHub.E_CombatEnd -= CombatEnd;
        EventHub.E_InventoryChanged -= ResetRecordTimer;
        EventHub.E_EnergyAbsorbed -= ResetRecordTimer;
    }

    public void CombatStart()
    {
        model.IsCombatContinue = true;

        if(model.coroutine!=null)
            StopCoroutine(model.coroutine);
        model.ShouldShowRecord = false;
        HideRecord();

        if (!model.IsRecordActive)
            StartRecord();
    }

    public void CombatEnd()
    {
        model.IsCombatContinue = false;
        model.coroutine = StartCoroutine(RecordShowOperations());
    }

    private void StartRecord()
    {
        model.IsRecordActive = true;
        model.RecordStartElectricity = CalculatePlayerTotalElectricity();
    }

    private void EndRecord()
    {
        model.IsRecordActive = false;
    }
    
    private double CalculatePlayerTotalElectricity()
    {
        return EnergyManager.Instance.GetElectricity() + GetEnergyInInventory();
    }

    private double GetEnergyInInventory()
    {
        return 0;
    }

    IEnumerator RecordShowOperations()
    {
        model.ShouldShowRecord = false;

        yield return new WaitForSeconds(model.timeInitialShowDelay);

        model.ShouldShowRecord = true;
        ResetRecordTimer();

        while (model.timer > 0)
        {
            yield return null;
            model.timer-= Time.deltaTime;
            model.timerShow -= Time.deltaTime;

            if (model.timerShow < 0)
                HideRecord();
        }

        EndRecord();
        model.ShouldShowRecord = false;
    }

    private void TryShowRecord()
    {
        if (model.IsCombatContinue)
            return;
        if(model.IsRecordActive & model.ShouldShowRecord)
            ShowRecord();
        model.timerShow = model.MaxTimerShow;
    }

    private void ShowRecord()
    {
        model.IsRecordShowing = true;
        //Add ShowRecord
    }
    private void HideRecord()
    {
        if (!model.IsRecordShowing)
            return;
        model.IsRecordShowing = false;
       //Add Stop Showing record
    }
    private void ResetRecordTimer()
    {
        model.timer = model.timeRecordEndAfter;

        TryShowRecord();
    }
}
