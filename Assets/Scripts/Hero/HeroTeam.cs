using System.Collections.Generic;

namespace DefaultNamespace.Hero
{
    public class HeroTeam
    {
        private readonly IHeroFactory _factory;
        
        public readonly List<Entity> Heroes = new();

        public TeamType TeamType { get; private set; }

        public int ActiveHeroIndex { get; private set; } = -1;

        public HeroTeam(TeamType teamType, IHeroFactory factory)
        {
            TeamType = teamType;
            _factory = factory;
        }
        
        public Entity AddNewHero()
        {
            Entity entity = _factory.GetNextHero();
            entity.AddComponent(new Team { Value = (byte)TeamType });
            
            Heroes.Add(entity);
            
            return entity;
        }
        public void RemoveEntity(Entity entity)
        {
            if(Heroes.Contains(entity) == false) return;
            Heroes.Remove(entity);
        }

        public void MoveActiveHeroNext()
        {
            if (++ActiveHeroIndex >= Heroes.Count) ActiveHeroIndex = -1;
        }
    }
}