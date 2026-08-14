using System;

public class TurnRightBlock : CodeBlock
{
    void Start()
    {
        SetQuantifier(1);
        blockType = BlockType.TURN;
    }

    public override void Activate(Action onFinished)
    {
        Decrement();
        robot.TurnRight(onFinished);
    }
}
