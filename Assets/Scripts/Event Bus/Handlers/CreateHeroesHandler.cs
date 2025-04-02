using System;
using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public sealed class CreateHeroesHandler: IEventReceiver<CreateHeroesEvent>, IInitializable, IDisposable
    {
        private readonly HeroTeamsService _heroTeamsService;
        private readonly EventBus _eventBus;
        
        private readonly IObjectResolver _resolver;
        private readonly TurnVisualPipeline _turnVisualPipeline;
        
        public UniqueId Id { get; } = new ();
        
        public CreateHeroesHandler(HeroTeamsService heroTeamsService, EventBus eventBus, 
            IObjectResolver resolver, TurnVisualPipeline turnVisualPipeline)
        {
            _heroTeamsService = heroTeamsService;
            _eventBus = eventBus;
            _resolver = resolver;
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


            for (var i = 0; i < heroes.Count; i++)
            {
                Entity hero = heroes[i];
                
                _turnVisualPipeline.AddTask(new ShowHeroViewTask(hero, _resolver));
            }
        }

        public void Initialize() => Enter();
        public void Dispose() => Exit();
    }
}