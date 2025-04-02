using System;
using DefaultNamespace.Event_Bus.Events;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class RechargeFightHandler: IEventReceiver<RechargeFightEvent>, IInitializable, IDisposable
    {
        private EventBus _eventBus;
        
        public UniqueId Id { get; } = new();

        public RechargeFightHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void OnEvent(RechargeFightEvent evt)
        {
            var defenderHealth = evt.Defender.GetComponent<Health>().Value;

            if (defenderHealth <= 0)
            {
                _eventBus.Raise(new DeathEvent(evt.Defender));
                return;
            }
            
            _eventBus.Raise(new FightEvent(evt.Defender, evt.Attacker));
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