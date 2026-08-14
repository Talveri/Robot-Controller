using System;
using System.Collections;
using UnityEngine;

public enum FacingDirection { Down, Left, Up, Right }
public class Robot : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gridSize = 1f;
    [SerializeField] private float baseMoveCost = 10;
    [SerializeField] private float baseTurnCost = 5;
    [SerializeField] private FacingDirection startFacing = FacingDirection.Right;
    [SerializeField] private EnergySystem energySystem;

    public FacingDirection Facing { get; private set; }
    public EnergySystem Energy => energySystem;

    public static event Action<bool> OnMove;
    public static event Action<FacingDirection> OnTurn;

    private Vector3 _startPosition;
    private bool _isActing;

    public Vector2Int GridPosition => new Vector2Int(
        Mathf.FloorToInt(transform.position.x / gridSize),
        Mathf.FloorToInt(transform.position.y / gridSize)
    );

    void Awake()
    {
        _startPosition = transform.position;
        Facing = startFacing;
        ApplyFacingRotation();
    }

    void OnEnable() => ButtonEvents.OnRestart += HandleReset;
    void OnDisable() => ButtonEvents.OnRestart -= HandleReset;

    public void MoveForward(Action onFinished)
    {
        if (_isActing) { onFinished?.Invoke(); return; }
        StartCoroutine(MoveRoutine(onFinished));
    }

    public void TurnRight(Action onFinished)
    {
        if (_isActing) { onFinished?.Invoke(); return; }
        StartCoroutine(TurnRoutine(+1, onFinished));
    }

    public void TurnLeft(Action onFinished)
    {
        if (_isActing) { onFinished?.Invoke(); return; }
        StartCoroutine(TurnRoutine(-1, onFinished));
    }

    
    private IEnumerator MoveRoutine(Action onFinished)
    {
        _isActing = true;

        GridManager gm = GridManager.Instance;
        Vector2Int targetGrid = GridPosition + FacingToVector(Facing);
        bool canMove = gm == null || gm.IsPassable(targetGrid);

        float totalCost = baseMoveCost + (canMove && gm != null ? gm.GetExtraEnergyCost(targetGrid) : 0f);

        if (energySystem != null && !energySystem.ConsumeEnergy(totalCost))
        {

            yield return new WaitForSeconds(0.1f); // Added Delay when no energy

            _isActing = false;
            onFinished?.Invoke();
            yield break;
        }

        OnMove?.Invoke(true);

        if (canMove)
        {

            Vector3 targetWorld = gm != null
                ? gm.GridToWorld(targetGrid, transform.position.z)
                : transform.position + new Vector3(FacingToVector(Facing).x * gridSize,
                                                   FacingToVector(Facing).y * gridSize, 0f);

            while (Vector3.Distance(transform.position, targetWorld) > 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetWorld, moveSpeed * Time.deltaTime);
                yield return null;
            }
            transform.position = targetWorld;


        }
        else
        {
            Vector3 origin = transform.position;
            Vector2Int fv = FacingToVector(Facing);
            Vector3 bump = origin + new Vector3(fv.x, fv.y, 0f) * (gridSize * 0.15f);
            yield return StartCoroutine(NudgeBetween(origin, bump, 0.15f));
        }
        OnMove?.Invoke(false);

        _isActing = false;
        onFinished?.Invoke();
    }

    private IEnumerator TurnRoutine(int direction, Action onFinished)
    {
        _isActing = true;

        if (energySystem != null && !energySystem.ConsumeEnergy(baseTurnCost))
        {
            yield return new WaitForSeconds(0.1f); // Added Delay when no energy

            _isActing = false;
            onFinished?.Invoke();
            yield break;
        }

        Facing = (FacingDirection)(((int)Facing + direction + 4) % 4);
        OnTurn?.Invoke(Facing);
        //ApplyFacingRotation();
        yield return new WaitForSeconds(0.25f);
        _isActing = false;
        onFinished?.Invoke();

    }

    private IEnumerator NudgeBetween(Vector3 origin, Vector3 bump, float duration)
    {
        float half = duration * 0.5f;
        for (float t = 0f; t < half; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(origin, bump, t / half);
            yield return null;
        }
        for (float t = 0f; t < half; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(bump, origin, t / half);
            yield return null;
        }
        transform.position = origin;
    }

    private void HandleReset()
    {
        StopAllCoroutines();
        _isActing = false;
        transform.position = _startPosition;
        Facing = startFacing;
        ApplyFacingRotation();
        energySystem?.FullRestore();
    }

    private void ApplyFacingRotation() =>
        transform.rotation = Quaternion.Euler(0f, 0f, FacingToAngle(Facing));

    private static Vector2Int FacingToVector(FacingDirection dir) => dir switch
    {
        FacingDirection.Up => Vector2Int.up,
        FacingDirection.Down => Vector2Int.down,
        FacingDirection.Left => Vector2Int.left,
        FacingDirection.Right => Vector2Int.right,
        _ => Vector2Int.zero
    };

    private static float FacingToAngle(FacingDirection dir) => dir switch
    {
        FacingDirection.Down => 0f,
        FacingDirection.Right => 90f,
        FacingDirection.Up => 180f,
        FacingDirection.Left => -90f,
        _ => 0f
    };
}

