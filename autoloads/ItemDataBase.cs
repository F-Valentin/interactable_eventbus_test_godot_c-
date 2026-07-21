using System.Collections.Generic;
using Godot;

public partial class ItemDataBase : Node
{
    public static ItemDataBase Instance { get; private set; } = null!;
    
    private string[] _ItemsPath = [
        "resources/bomb.tres",
        "resources/key.tres",
    ];

    private Dictionary<StringName, ItemData> _lookup = [];

    public override void _Ready()
    {
        Instance = this;
        
        foreach (string path in _ItemsPath)
        {
            ItemData itemData = GD.Load<ItemData>(path);

            _lookup[itemData.id] = itemData;
        }
    }

    public ItemData? GetItemData(StringName id)
    {
        _lookup.TryGetValue(id, out ItemData? itemData);

        return itemData;
    }
}