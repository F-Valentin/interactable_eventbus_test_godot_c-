public interface IInteractable
{
    bool IsInteractable { get; }
    InteractionCommand? Interact();
}