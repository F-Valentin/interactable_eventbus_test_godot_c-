using System;

public static class EventBus
{
    public static event Action? ItemPickUp;

    public static void EmitItemPickUp() => ItemPickUp?.Invoke();
}