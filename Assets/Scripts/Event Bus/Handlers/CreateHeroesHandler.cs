using System;
using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;
using UI;
using UnityEngine;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public sealed class CreateHeroesHandler: IEventReceiver<CreateHeroesEvent>, IInitializable, IDisposable
    {
        private readonly HeroTeamsService _heroTeamsService;
        private readonly EventBus _eventBus;
        
        private readonly UIService _uiService;
        private readonly TurnVisualPipeline _turnVisualPipeline;
        
        public UniqueId Id { get; } = new ();
        
        public CreateHeroesHandler(HeroTeamsService heroTeamsService, EventBus eventBus, 
            UIService uiService, TurnVisualPipeline turnVisualPipeline)
        {
            _heroTeamsService = heroTeamsService;
            _eventBus = eventBus;
            _uiService = uiService;
            _turnVisualPipeline = turnVisualPipeline;
        }

        public void Enter()
        {
            _eventBus.Register(this);
        }

        public void Exit()
        {
            _eventBus.Unregister(this);
        }
        
        public void OnEvent(CreateHeroesEvent evt)
        {
            var heroes = 
                _heroTeamsService.AddNewHeroes(evt.TeamType, evt.HeroCount);

            var heroesView = evt.TeamType == Hero.Team.Blue
                    ? _uiService.GetBluePlayer()
                    : _uiService.GetRedPlayer();
            
            for (var i = 0; i < heroes.Count; i++)
            {
                HeroView heroView = heroesView.GetView(i);
                Entity hero = heroes[i];
                
                _turnVisualPipeline.AddTask(new UpdateHeroViewTask(hero, heroView));
            }
        }

        public void Initialize() => Enter();
        public void Dispose() => Exit();
    }
}