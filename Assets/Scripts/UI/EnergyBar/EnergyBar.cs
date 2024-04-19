using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private Image EnergyImage;
    private void Awake()
    {
        EventHub.E_ElectricityChangedNewRate += SetEnergeBarImageFill;
    }

    private void OnDestroy()
    {
        EventHub.E_ElectricityChangedNewRate -= SetEnergeBarImageFill;
    }
    private void SetEnergeBarImageFill(float rate)
    {
        EnergyImage.fillAmount = rate;
    }
}
