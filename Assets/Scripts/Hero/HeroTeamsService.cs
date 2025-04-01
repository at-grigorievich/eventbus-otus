using System;
using System.Collections.Generic;

namespace DefaultNamespace.Hero
{
    public enum Team: byte
    {
        None = 0,
        Red = 1,
        Blue = 2
    }
    
    public class HeroTeamsService
    {
        private readonly IHeroFactory _heroFactory;
        
        public readonly Dictionary<Team, List<Entity>> Heroes = new ();

        public HeroTeamsService(IHeroFactory heroCreator)
        {
            _heroFactory = heroCreator;
        }

        public IReadOnlyList<Entity> AddNewHeroes(Team team, int count)
        {
            List<Entity> heroes = new(count);
            
            for (int i = 0; i < count; i++)
            {
                heroes.Add(AddNewHero(team));
            }

            return heroes;
        }

        public List<Entity> GetHeroes(Team team)
        {
            if (Heroes.ContainsKey(team) == false)
            {
                return new List<Entity>();
            }

            return Heroes[team];
        }
        
        public Entity AddNewHero(Team team)
        {
            Entity entity = _heroFactory.GetNextHero();
            entity.AddComponent(new DefaultNamespace.Team { Value = (byte)team });

            if (Heroes.ContainsKey(team) == false)
            {
                Heroes.Add(team, new List<Entity>(1));
            }
            
            Heroes[team].Add(entity);

            return entity;
        }

        public void RemoveHero(Team team)
        {
            throw new NotImplementedException();
        }
    }
}