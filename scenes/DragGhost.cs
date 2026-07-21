using Godot;
using System;

public partial class DragGhost : CanvasLayer
{
    private TextureRect _dragIcon = null!;
    
    public override void _Ready()
    {
        _dragIcon = GetNode<TextureRect>("DragIcon");
    }
    
	public override void _Process(double delta)
	{
        if (InventoryManager.Instance.HeldItemData != null)
        {
            _dragIcon.Texture = InventoryManager.Instance.HeldItemData.Icon;
            _dragIcon.Visible = true;
            _dragIcon.GlobalPosition = GetViewport().GetMousePosition() - new Vector2(20.0f, 20.0f);
        }
        else
            _dragIcon.Visible = false;
	}
}
