using Godot;
using System;

[GlobalClass]
public partial class ItemData : Resource
{
    public enum Category { Weapon, Armor, Consumable, Material, Misc };

    [Export]
    public StringName id = "";
    [Export]
    public string DisplayName { get; set; } = "";
    [Export(PropertyHint.MultilineText)]
    public string Description { get; set; } = "";
    [Export]
    public Texture2D? Icon { get; set; } = null;
    [Export]
    public Category ItemCategory { get; set; } = Category.Misc;
    [Export]
    public int MaxStackSize { get; set; } = 1;
}
