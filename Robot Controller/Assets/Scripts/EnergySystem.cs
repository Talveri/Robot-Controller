using UnityEngine;
using UnityEngine.Events;

public class EnergySystem : MonoBehaviour
{
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float startingEnergy = 100f;

    public UnityEvent<float> onEnergyChanged;
    public UnityEvent onEnergyDepleted;

    private float currentEnergy;

    public float CurrentEnergy => currentEnergy;
    public float MaxEnergy => maxEnergy;
    public float EnergyPercent => currentEnergy / maxEnergy;
    public bool IsDepleted => currentEnergy <= 0f;

    void Awake()
    {
        currentEnergy = Mathf.Clamp(startingEnergy, 0f, maxEnergy);
    }

    public bool HasEnergy(float cost = 0f) => currentEnergy > cost;

public bool ConsumeEnergy(float amount)
    {
        if (IsDepleted)
            return false;
        currentEnergy = Mathf.Max(0f, currentEnergy - amount);
        onEnergyChanged?.Invoke(currentEnergy);
        if (IsDepleted)
            onEnergyDepleted?.Invoke();
        return !IsDepleted;
    }
    
    public void RestoreEnergy(float amount)
    {
        currentEnergy = Mathf.Min(maxEnergy, currentEnergy + amount);
        onEnergyChanged?.Invoke(currentEnergy);
    }

    public void FullRestore()
    {
        currentEnergy = maxEnergy;
        onEnergyChanged?.Invoke(currentEnergy);
    }
}
