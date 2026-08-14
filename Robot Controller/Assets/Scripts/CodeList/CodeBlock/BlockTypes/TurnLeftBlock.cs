using System;

public class TurnLeftBlock : CodeBlock
{
    void Start()
    {
        SetQuantifier(1);
        blockType = BlockType.TURN;
    }

    public override void Activate(Action onFinished)
    {
        Decrement();
        robot.TurnLeft(onFinished);
    }
}
