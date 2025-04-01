using UnityEngine;

namespace DefaultNamespace
{
    public interface IComponent {}
    
    public struct Name : IComponent
    {
        public string Value;
    }

    public struct Icon : IComponent
    {
        public Sprite Value;
    }

    public struct Description : IComponent
    {
        public string Value;
    }

    public struct Health : IComponent
    {
        public int Value;
    }

    public struct Damage : IComponent
    {
        public int Value;
    }

    public struct Team : IComponent
    {
        public byte Value;
    }

    public struct TeamIndex : IComponent
    {
        public int Value;
    }
}