public interface ICodeBlock
{
    Robot robot { get; set; }
    BlockType blockType { get; set; }
    void Activate(System.Action onFinished);
    int GetQuantifier();
}
