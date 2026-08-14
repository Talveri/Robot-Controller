using System;

public class ChargeBlock : CodeBlock
{
    void Start() => blockType = BlockType.CHARGE;

    public override void Activate(Action onFinished)
    {   
        ChargeRoutine(onFinished);
    }

    private void ChargeRoutine(Action onFinished)
    {
        GridManager gm = GridManager.Instance;

        if (robot != null && robot.Energy != null && gm != null && gm.GetTerrainType(robot.GridPosition) == TerrainType.Goal)
        {
            // int subscribers = ButtonEvents.OnGoalReached?.GetInvocationList().Length ?? 0;
            // Debug.Log($"Firing OnGoalReached — subscriber count: {subscribers}");
            ButtonEvents.OnGoalReached?.Invoke();
        }
        onFinished?.Invoke();
    }
}
