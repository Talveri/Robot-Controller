using System;
using TMPro;
using UnityEngine;

public class EnergyUsageInputFieldScript : MonoBehaviour
{
    public TMP_InputField quantifierInput;
    public TMP_Text energyText;
    public int baseEnergyUse;

    void Start() => energyText.text = baseEnergyUse.ToString();

    public void UpdateEnergy()
    {
        int.TryParse(quantifierInput.text, out int quantity);
        quantity = Math.Clamp(quantity, 1, 10);
        quantifierInput.text = quantity.ToString();
        energyText.text = (baseEnergyUse * quantity).ToString();
    }
}
