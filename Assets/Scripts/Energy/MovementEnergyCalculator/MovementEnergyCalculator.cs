using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEnergyCalculator : MonoBehaviour
{
    #region Instantiate And Destroy
    public static MovementEnergyCalculator Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        model = new MovementEnergyCalculatorModel();
        EventHub.E_PlayerMoveSpeed += MovementEnergyExpenditure;
    }
    private void OnDestroy()
    {
        EventHub.E_PlayerMoveSpeed -= MovementEnergyExpenditure;
    }
    #endregion

    private MovementEnergyCalculatorModel model;

    private void MovementEnergyExpenditure(Vector2 speed)
    {
        float speedMagnitude = speed.magnitude;
        float expenditure = Mathf.Pow(speedMagnitude,2) * model.MovementElectricityExpenditure * model.MultiplyConstant * Time.deltaTime;

        EnergyManager.Instance.RemoveEnergy(expenditure);
    }

    public void SetExpenditureStat(float ExpenditureStat)
    {
        if(ExpenditureStat <= 0) 
        {
            print("dont make this less than 0 wtf man");
            return;
        }

        model.MovementElectricityExpenditure = ExpenditureStat;
    }
}
