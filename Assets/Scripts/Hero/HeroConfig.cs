﻿using UnityEngine;

namespace DefaultNamespace.Hero
{
    [CreateAssetMenu(fileName = "new hero", menuName = "Heroes/New Hero")]
    public class HeroConfig : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }

        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int Health { get; private set; }

        public Entity CreateHeroEntity() => new Entity()
                .AddComponent(new Name { Value = Name })
                .AddComponent(new Icon { Value = Icon })
                .AddComponent(new Description { Value = Description })
                .AddComponent(new Damage { Value = Damage })
                .AddComponent(new Health { Value = Health });
    }
}