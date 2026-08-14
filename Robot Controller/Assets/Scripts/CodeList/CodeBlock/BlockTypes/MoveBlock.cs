using System;

public class MoveBlock : CodeBlock
{
    void Start()
    {
        SetQuantifier(1);
        blockType = BlockType.MOVE;
    }

    public override void Activate(Action onFinished)
    {
        Decrement();
        robot.MoveForward(onFinished);
    }
}
