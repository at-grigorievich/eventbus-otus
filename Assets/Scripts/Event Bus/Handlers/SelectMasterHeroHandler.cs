using System;
using System.Collections.Generic;
using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;
using UI;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class SelectMasterHeroHandler: EventReceiver<NextMoveEvent>, IInitializable, IDisposable
    {
        private readonly HeroTeamsService _heroTeamsService;
        
        private readonly UIService _uiService;
        private readonly TurnVisualPipeline _turnVisualPipeline;

        public SelectMasterHeroHandler(EventBus eventBus, HeroTeamsService heroTeamsService, 
            TurnVisualPipeline turnVisualPipeline, UIService uiService): base(eventBus)
        {
            _heroTeamsService = heroTeamsService;
            
            _turnVisualPipeline = turnVisualPipeline;
            _uiService = uiService;
        }
        
        public override void OnEvent(NextMoveEvent evt)
        {
            TeamType currentMasterTeam = _heroTeamsService.SwapMoveTeam();
            
            _heroTeamsService[currentMasterTeam].MoveActiveHeroNext();
            
            RemoveMasterMarkerFromWaitTeam();
            AddMarkerHeroInMasterTeam();
            
           _turnVisualPipeline.AddTask(
               new SetMasterHeroViewMarkerTask(_heroTeamsService, _uiService)); 
        }

        public void Initialize() => Enter();

        public void Dispose() => Exit();
        
        private void RemoveMasterMarkerFromWaitTeam()
        {
            TeamType waitTeam = _heroTeamsService.MasterTeam == TeamType.Blue ? TeamType.Red : TeamType.Blue;
            IReadOnlyList<Entity> waiters = _heroTeamsService[waitTeam].Heroes;

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
                    _eventBus.Raise(new PlayRandomTurnSoundEvent(attackers[i]));
                }
                else
                {
                    attackers[i].RemoveComponent<MasterMarker>();
                }
            }
            
        }
    }
}