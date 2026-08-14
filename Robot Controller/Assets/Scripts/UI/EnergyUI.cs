using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] private EnergySystem energySystem;
    [SerializeField] private Image energyBarFill;
    [SerializeField] private TextMeshProUGUI energyText;

    [SerializeField] private Color normalColor = new Color(0.2f, 0.8f, 0.3f);
    [SerializeField] private Color lowColor    = new Color(0.9f, 0.3f, 0.2f);
    [SerializeField] [Range(0f, 1f)] private float lowEnergyThreshold = 0.25f;

    void Start()
    {
        energySystem.onEnergyChanged.AddListener(UpdateDisplay);
        UpdateDisplay(energySystem.CurrentEnergy);
    }

    void OnDestroy() => energySystem.onEnergyChanged.RemoveListener(UpdateDisplay);

    private void UpdateDisplay(float currentEnergy)
    {
        float percent = energySystem.EnergyPercent;
        Color color = percent <= lowEnergyThreshold ? lowColor : normalColor;

        if (energyBarFill != null)
        {
            energyBarFill.fillAmount = percent;
            energyBarFill.color = color;
        }

        if (energyText != null)
        {
            energyText.text = $"{Mathf.CeilToInt(currentEnergy)} / {Mathf.CeilToInt(energySystem.MaxEnergy)}";
            energyText.color = color;
        }
    }
}
