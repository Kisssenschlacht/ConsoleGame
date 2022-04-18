using System;

namespace ConsoleGame
{
    partial class Program
    {
    int randomEntitySpawner = new Random().Next();
        public List<Entity> Entities { get => _state.Entities; set => _state.Entities = value; }
    }
}