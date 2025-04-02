using System;
using DefaultNamespace.Event_Bus.Events;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class RechargeFightHandler: EventReceiver<RechargeFightEvent>, IInitializable, IDisposable
    {
        public RechargeFightHandler(EventBus eventBus): base(eventBus) { }
        
        public override void OnEvent(RechargeFightEvent evt)
        {
            var defenderHealth = evt.Defender.GetComponent<Health>().Value;

            if (defenderHealth <= 0)
            {
                _eventBus.Raise(new DeathEvent(evt.Defender));
                return;
            }
            
            _eventBus.Raise(new FightEvent(evt.Defender, evt.Attacker));
        }

        public void Initialize() => Enter();
        public void Dispose() => Exit();
    }
}