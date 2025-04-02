using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;
using UI;
using UnityEngine;
using VContainer;

namespace DefaultNamespace
{
    public class SelectTargetByClickTask: EventTask
    {
        private readonly EventBus _eventBus;
        
        private readonly UIService _uiService;
        private readonly HeroTeamsService _heroTeamsService;

        private HeroListView _defenderHeroesView;
        private TeamType _defenderTeamType;
        
        public SelectTargetByClickTask(IObjectResolver resolver)
        {
            _eventBus = resolver.Resolve<EventBus>();
            _uiService = resolver.Resolve<UIService>();
            _heroTeamsService = resolver.Resolve<HeroTeamsService>();
        }
        
        protected override void OnStart()
        {
            _defenderTeamType = _heroTeamsService.MasterTeam == 
                                TeamType.Blue ? TeamType.Red : TeamType.Blue;
            _defenderHeroesView = _defenderTeamType == TeamType.Red 
                ? _uiService.GetRedPlayer() 
                : _uiService.GetBluePlayer();
            
            _defenderHeroesView.OnHeroClicked += OnHeroClicked;
        }

        private void OnHeroClicked(HeroView obj)
        {
            _defenderHeroesView.OnHeroClicked -= OnHeroClicked;

            Entity attacker = GetAttackerEntity();
            Entity defender = GetDefenderEntity(obj);
            
            _eventBus.Raise(new FightEvent(attacker, defender));
            _eventBus.Raise(new FightEvent(defender, attacker));
            
            Complete();
        }

        private Entity GetAttackerEntity()
        {
            HeroTeam teamAttacker = _heroTeamsService[_heroTeamsService.MasterTeam];
            Entity attacker = teamAttacker.Heroes[teamAttacker.ActiveHeroIndex];

            return attacker;
        }

        private Entity GetDefenderEntity(HeroView obj)
        {
            HeroTeam teamDefender = _heroTeamsService[_defenderTeamType];
            Entity defender = teamDefender.Heroes[GetDefenderIndex()];
            
            return defender;
            
            int GetDefenderIndex()
            {
                int defender = 0;
                foreach (var view in _defenderHeroesView.GetViews())
                {
                    if (ReferenceEquals(obj, view) == false)
                    {
                        defender++;
                    }
                    else break;
                }
                
                return defender;
            }
        }
    }
}