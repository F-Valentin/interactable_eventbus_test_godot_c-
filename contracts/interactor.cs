using Godot;

public interface IActor;
// Narrow capability interfaces — implement only what your actor actually supports
public interface IInventoryHolder : IActor
{
    Inventory Inventory { get; }
}

public interface ICarryCapable : IActor
{
    Node2D CarryAnchor { get; }
    // StateMachine StateMachine { get; }
}

public interface IEquipmentHolder : IActor
{
    // EquipmentSlots EquipmentSlots { get; }
}