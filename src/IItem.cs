using System.Diagnostics.CodeAnalysis;

namespace ConsoleGame
{
    interface IItem : IUpdatable
    {
        ItemComparer GetItemComparer() => new ItemComparer() { Type = GetType() };
        ulong MaxStackSize();
        string DisplayName();
    }
    struct ItemComparer
    {
        public Type Type;
        public static bool operator ==(ItemComparer a, ItemComparer b) => a.Type == b.Type;
        public static bool operator !=(ItemComparer a, ItemComparer b) => a.Type != b.Type;

        public override bool Equals(object? obj) => obj != null && obj is ItemComparer ? this == (ItemComparer)obj : false;

        public override int GetHashCode() => this.Type.GetHashCode();
    }
}