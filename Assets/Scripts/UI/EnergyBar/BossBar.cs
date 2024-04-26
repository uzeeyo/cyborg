using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] BossHealth health;
    [SerializeField] private Image EnergyImage;

    void Start()
    {
        health.E_HealtRateChanged += SetEnergeBarImageFill;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetEnergeBarImageFill(float rate)
    {
        EnergyImage.fillAmount = rate;
    }
}
