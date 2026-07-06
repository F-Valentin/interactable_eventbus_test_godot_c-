using Godot;

public interface Interactor
{
    public Item Item { get; }
    public bool CanInteract { get; }

    public void OnItemEntered(Item item);
}