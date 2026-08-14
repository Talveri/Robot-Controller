using System;
using TMPro;
using UnityEngine;

public class CodeBlockEnergyDisplay : MonoBehaviour
{
    private TMP_InputField quantifierInput;
    private TMP_Text energyText;
    [SerializeField] private int baseEnergyUse;
    void Start()
    {
        energyText = GetComponentInChildren<TMP_Text>();
        UpdateEnergy();
    }
    public void SetQuantifierInputfield(TMP_InputField tMP_InputField) => quantifierInput = tMP_InputField;

    public void UpdateEnergy()
    {
        if (quantifierInput == null) return;
        int.TryParse(quantifierInput.text, out int quantity);

        quantity = Math.Clamp(quantity,1,10);
        quantifierInput.text = quantity.ToString();

        energyText.text = CalculateEnergyValue(quantity).ToString();
    }

    private int CalculateEnergyValue(int quantity = 1)
    {
        return baseEnergyUse * quantity;
    }

}
