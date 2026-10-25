public interface IInteractable
{
    public toolTypes toolRequired { get; set; }
    void Execute(toolTypes usedTool);
}
public interface IWaterable
{

}