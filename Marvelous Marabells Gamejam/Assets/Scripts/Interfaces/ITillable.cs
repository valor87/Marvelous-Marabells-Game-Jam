public interface IInteraction
{
    public toolTypes toolRequired { get; set; }
    void Execute(toolTypes usedTool);
}
