public abstract record Item;

public record Weapon(string name, float damage, float weight) : Item;