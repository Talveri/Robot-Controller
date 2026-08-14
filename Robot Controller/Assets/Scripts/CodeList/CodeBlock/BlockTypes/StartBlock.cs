using System;

public class StartBlock : CodeBlock
{
    void Start() => blockType = BlockType.START;

    public override void Activate(Action onFinished) => onFinished?.Invoke();
}
