using System.Collections.Generic;

namespace DefaultNamespace.Hero
{
    public enum TeamType: byte
    {
        None = 0,
        Red = 1,
        Blue = 2
    }
    
    public class HeroTeamsService
    {
        private readonly IHeroFactory _heroFactory;
        
        public readonly Dictionary<TeamType, HeroTeam> Teams = new ();
        
        public TeamType MasterTeam { get; set; } = TeamType.None;
        
        public HeroTeam this[TeamType val] => Teams.ContainsKey(val) ? Teams[val] : null;
        
        public HeroTeamsService(IHeroFactory heroCreator)
        {
            _heroFactory = heroCreator;
        }
        
        #region Add/Get/Remove Heroes
        public IReadOnlyList<Entity> AddNewHeroes(TeamType teamType, int count)
        {
            for (int i = 0; i < count; i++)
            {
                AddNewHero(teamType);
            }
            
            return Teams[teamType].Heroes;
        }

        public IReadOnlyList<Entity> GetHeroes(TeamType teamType)
        {
            return Teams.TryGetValue(teamType, value: out var team) == false 
                ? new List<Entity>() 
                : team.Heroes;
        }
        
        public Entity AddNewHero(TeamType teamType)
        {
            if (Teams.ContainsKey(teamType) == false)
            {
                Teams.Add(teamType, new HeroTeam(teamType, _heroFactory));
            }

            return Teams[teamType].AddNewHero();
        }

        public void RemoveHero(TeamType teamType, Entity entity)
        {
            if (Teams.TryGetValue(teamType, out var team) == false) return;
            team.RemoveEntity(entity);
        }
        #endregion

        public void MoveMarkerToNextHero(TeamType teamType)
        {
            if (Teams.TryGetValue(teamType, out var team) == false) return;
            team.MoveActiveHeroNext();
        }
        
        public TeamType SwapMoveTeam()
        {
            if (MasterTeam== TeamType.None) MasterTeam = SelectFirstMoveTeam();
            MasterTeam = MasterTeam == TeamType.Blue ? TeamType.Red : TeamType.Blue;

            return MasterTeam;
        }
        
        private TeamType SelectFirstMoveTeam()
        {
            int rnd = UnityEngine.Random.Range(0, 11);
            
            if(rnd % 2 == 0) return TeamType.Blue;
            return TeamType.Red;
            
        }
    }
}