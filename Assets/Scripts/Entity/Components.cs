﻿using UnityEngine;

namespace DefaultNamespace
{
    public interface IComponent {}
    
    public class Name : IComponent
    {
        public string Value;
    }

    public class Icon : IComponent
    {
        public Sprite Value;
    }

    public class Description : IComponent
    {
        public string Value;
    }

    public class Health : IComponent
    {
        public int Value;
    }

    public class Damage : IComponent
    {
        public int Value;
    }

    public class Team : IComponent
    {
        public byte Value;
    }

    public class ViewIndex : IComponent
    {
        public int Value;
    }
    
    public class MasterMarker : IComponent {}
    
    public class DeathMarker: IComponent {}
}