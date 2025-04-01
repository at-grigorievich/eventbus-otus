using System;
using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public sealed class CreateHeroesHandler: IEventReceiver<CreateHeroesEvent>, IInitializable, IDisposable
    {
        private readonly HeroTeamsService _heroTeamsService;
        private readonly EventBus _eventBus;
        
        public UniqueId Id { get; } = new UniqueId();
        
        public CreateHeroesHandler(HeroTeamsService heroTeamsService, EventBus eventBus)
        {
            _heroTeamsService = heroTeamsService;
            _eventBus = eventBus;
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
            _heroTeamsService.AddNewHeroes(evt.TeamType, evt.HeroCount);
        }

        public void Initialize() => Enter();
        public void Dispose() => Exit();
        
    }
}