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

        public void AddNewHeroes(Team team, int count)
        {
            for (int i = 0; i < count; i++)
            {
                AddNewHero(team);
            }
        }
        
        public void AddNewHero(Team team)
        {
            Entity entity = _heroFactory.GetNextHero();
            entity.AddComponent(new DefaultNamespace.Team { Value = (byte)team });

            if (Heroes.ContainsKey(team) == false)
            {
                Heroes.Add(team, new List<Entity>(1));
            }
            
            Heroes[team].Add(entity);
        }

        public void RemoveHero(Team team)
        {
            throw new NotImplementedException();
        }
    }
}