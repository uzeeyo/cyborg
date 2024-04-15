using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnergyManager 
{
    #region Instantiation
    private static EnergyManager instance;

    public static EnergyManager Instance
    {
        get
        {
            if (instance == null)
                instance = new EnergyManager();
            return instance;
        }
    }

    public EnergyManager()
    {
        model = new PlayerEnergyData();
    }
    #endregion

    private PlayerEnergyData model;

    public void AddEnergy(float energy)
    {
        model.Electricity += energy;
        model.Electricity = math.min(model.Electricity, model.MaxElectricity);

        FireElectricityRateChangedEvent();
    }
    public bool TryToRemoveEnergy(float energy)
    {
        if(model.Electricity <= energy)
            return false;

        RemoveEnergy(energy);
        return true;
    }
    public void RemoveEnergy(float energy)
    {
        model.Electricity -= energy;

        if (model.Electricity <= 0)
        {
            model.Electricity = 0;
            EnergyIsZero();
        }
        FireElectricityRateChangedEvent();
    }
    private void EnergyIsZero()
    {
        EventHub.EnergyEnded();
    }
    public void SetMaxElectricity(double maxElectricity)
    {
        model.MaxElectricity = maxElectricity;
        FireElectricityRateChangedEvent();
    }
    private void FireElectricityRateChangedEvent()
    {     
        EventHub.ElectricityChangedNewRate(GetElectricityRate());
    }

    public float GetElectricityRate()
    {
        double Rate = model.Electricity / model.MaxElectricity;
        return (float)Rate;
    }
    public double GetElectricity()
    {
        return model.Electricity;
    }

}
