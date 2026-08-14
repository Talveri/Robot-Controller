
using System.Collections.Generic;
using UnityEngine;

public class CodeListLogic : MonoBehaviour
{
    [SerializeField] Pointer pointer;
    [SerializeField] Robot robot;

    [SerializeField] private List<CodeBlock> CodeBlocks = new List<CodeBlock>();
    [SerializeField] private GameObject pointerPrefab;
    private int _currentIndex;
    private int _currentRepeat;
    private CodeBlock _currentBlock;
    private bool _started;
    private const int MaxRepeats = 10;

    void OnEnable()
    {
        ButtonEvents.OnPlay += StartExecution;
        ButtonEvents.OnRestart += HandleReset;
    }

    void OnDisable()
    {
        ButtonEvents.OnPlay -= StartExecution;
        ButtonEvents.OnRestart -= HandleReset;
    }

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        CodeBlocks.Clear();
        foreach (Transform child in transform)
        {
            CodeBlock block = child.GetComponent<CodeBlock>();
            if (block != null)
            {
                CodeBlocks.Add(block);
                block.robot = robot;
                block.Initialize();
                
            }
            if(block.blockType == BlockType.CHARGE) break;  // Stop when charge is reached
        }
    }

    private void StartExecution()
    {
        if (_started) return;
        _started = true;
        Initialize();
        _currentIndex = CodeBlocks.FindIndex(b => b.blockType == BlockType.START);
        ExecuteNext();
    }

    private void ExecuteNext()
    {
        if (_currentIndex > CodeBlocks.FindIndex(b => b.blockType == BlockType.CHARGE))
        {
            Debug.Log($"{name} : Sequence Finished!");
            ButtonEvents.OnSequenceFinished?.Invoke();
            return;
        }
        _currentBlock = CodeBlocks[_currentIndex];
        pointer.SetPointer((MonoBehaviour)_currentBlock);

        _currentBlock.Activate(Finished);
    }


    private void Finished()
    {
        _currentRepeat++;
        if (_currentBlock.GetQuantifier() <= 0 || _currentRepeat > MaxRepeats)
        {
            _currentRepeat = 0;
            _currentIndex++;

            // Play Sound on switching line
            if (_currentIndex == CodeBlocks.Count - 1)
                CodeListEvents.OnLastCodeBlock?.Invoke();
            else if (_currentIndex < CodeBlocks.Count - 1)
                CodeListEvents.OnNextCodeBlock?.Invoke();
        }
        ExecuteNext();
    }

    bool PointerIsMissing => pointer == null || pointer.gameObject == null;

    private void HandleReset()
    {
        if (PointerIsMissing)
            pointer = Instantiate(pointerPrefab).GetComponent<Pointer>();
        pointer.SetPointer((MonoBehaviour)CodeBlocks[CodeBlocks.FindIndex(b => b.blockType == BlockType.START)]);
        _started = false;
    }
}
