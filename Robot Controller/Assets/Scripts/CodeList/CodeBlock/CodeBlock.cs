using System;
using TMPro;
using UnityEngine;

public abstract class CodeBlock : MonoBehaviour, ICodeBlock
{

    public Robot robot { get; set; }
    public BlockType blockType { get; set; }
    public CodeBlockEnergyDisplay codeBlockEnergyDisplay;


    public TMP_InputField quantity;
    private string savedQuantity;
    private int count = 0;

    void Awake()
    {
        
        if(codeBlockEnergyDisplay != null)
            codeBlockEnergyDisplay.SetQuantifierInputfield(quantity);
        if (quantity != null)
            quantity.text = count.ToString();
    }

    void OnEnable()
    {
        ButtonEvents.OnRestart += HandleReset;
    }


    //=================================================================
    //===EVENT HANDLING================================================
    //=================================================================
    void OnDisable()
    {
        ButtonEvents.OnRestart -= HandleReset;
    }

    public abstract void Activate(Action onFinished);

    public int GetQuantifier() => count;

    public void SetQuantifier(int value)
    {
        count = value;
        quantity.text = value.ToString();
        savedQuantity = value.ToString();

    }

    protected void Decrement()
    {
        count--;
        if (quantity != null)
        {
            quantity.text = count.ToString();
        }
        else
        {
            Debug.LogWarning($"{name}: No TMP_InputField found for quantity.", this);
        }
    }

    public void Initialize()
    {
        if (quantity != null)
        {
            if (!int.TryParse(quantity.text, out count))
                return;
            count = Math.Clamp(count, 1, 10);
            quantity.text = count.ToString();
            savedQuantity = quantity.text;
        }
        
    }

    private void HandleReset()
    {
        if (quantity != null)
            quantity.text = savedQuantity;
    }

    private void CacheQuantityField()
    {
        if (quantity == null)
            quantity = GetComponentInChildren<TMP_InputField>(true);
    }
}
