using System;
using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;
using UI;
using Unity.VisualScripting;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class StartFightHandler: IEventReceiver<StartFightEvent>, IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly UIService _uiService;
        private readonly HeroTeamsService _heroTeamsService;
        
        public UniqueId Id { get; } = new();

        public StartFightHandler(EventBus eventBus, UIService uiService, HeroTeamsService heroTeamsService)
        {
            _eventBus = eventBus;
            _uiService = uiService;
            _heroTeamsService = heroTeamsService;
        }
        
        public void OnEvent(StartFightEvent evt)
        {
            throw new NotImplementedException();
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
    }
}