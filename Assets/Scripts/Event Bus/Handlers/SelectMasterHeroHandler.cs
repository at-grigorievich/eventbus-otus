using System;
using System.Collections.Generic;
using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;
using UI;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class SelectMasterHeroHandler: IEventReceiver<NextMoveEvent>, IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly HeroTeamsService _heroTeamsService;
        
        private readonly UIService _uiService;
        private readonly TurnVisualPipeline _turnVisualPipeline;
        
        public UniqueId Id { get; } = new();

        public SelectMasterHeroHandler(EventBus eventBus, HeroTeamsService heroTeamsService, 
            TurnVisualPipeline turnVisualPipeline, UIService uiService)
        {
            _eventBus = eventBus;
            _heroTeamsService = heroTeamsService;
            
            _turnVisualPipeline = turnVisualPipeline;
            _uiService = uiService;
        }
        
        public void OnEvent(NextMoveEvent evt)
        {
            TeamType currentMasterTeam = _heroTeamsService.SwapMoveTeam();
            
            _heroTeamsService.MoveMarkerToNextHero(currentMasterTeam);
            
            RemoveMasterMarkerFromWaitTeam();
            AddMarkerHeroInMasterTeam();
            
           _turnVisualPipeline.AddTask(
               new SetMasterHeroViewMarkerTask(_heroTeamsService, _uiService)); 
        }

        public void Enter()
        {
            _eventBus.Register(this);
        }

        public void Exit()
        {
            _eventBus.Unregister(this);
        }

        public void Initialize() => Enter();

        public void Dispose() => Exit();

        private TeamType SelectFirstMoveTeam()
        {
            int rnd = UnityEngine.Random.Range(0, 11);
            
            if(rnd % 2 == 0) return TeamType.Blue;
            return TeamType.Red;
        }
        

        private void RemoveMasterMarkerFromWaitTeam()
        {
            TeamType waitTeam = _heroTeamsService.MasterTeam == TeamType.Blue ? TeamType.Red : TeamType.Blue;
            IReadOnlyList<Entity> waiters = _heroTeamsService.GetHeroes(waitTeam);

            foreach (var e in waiters)
            {
                e.RemoveComponent<MasterMarker>();
            }
        }

        private void AddMarkerHeroInMasterTeam()
        {
            var attackers = _heroTeamsService[_heroTeamsService.MasterTeam].Heroes;
            int requiredIndex = _heroTeamsService[_heroTeamsService.MasterTeam].ActiveHeroIndex;

            for (int i = 0; i < attackers.Count; i++)
            {
                if (i == requiredIndex)
                {
                    attackers[i].AddComponent(new MasterMarker());
                }
                else
                {
                    attackers[i].RemoveComponent<MasterMarker>();
                }
            }
            
        }
    }
}