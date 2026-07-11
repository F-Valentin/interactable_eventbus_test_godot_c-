using Godot;

public abstract class InteractionCommand()
{
    public abstract void Apply(Player player);
}

public sealed class TargetInfoCommand : InteractionCommand
{
    private readonly string _targetName;
    public TargetInfoCommand(string name)
    {
        _targetName = name;
    }
    public override void Apply(Player player)
    {
        GD.Print($"{player.Name} interact with {_targetName}");
    }
}


public sealed class PickUpItemCommand: InteractionCommand
{
    private readonly string _itemName;
    public PickUpItemCommand(string name)
    {
        _itemName = name;
    }
    public override void Apply(Player player)
    {
        GD.Print($"{player.Name} has obtained {_itemName}");
    }
}