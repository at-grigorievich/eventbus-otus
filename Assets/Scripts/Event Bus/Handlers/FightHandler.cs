using System;
using DefaultNamespace.Event_Bus.Events;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class FightHandler: IEventReceiver<FightEvent>, IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly FightVisualPipeline _fightVisualPipeline;
        private readonly IObjectResolver _resolver;
        
        public UniqueId Id { get; } = new();

        public FightHandler(EventBus eventBus, FightVisualPipeline fightVisualPipeline, 
            IObjectResolver resolver)
        {
            _eventBus = eventBus;
            _fightVisualPipeline = fightVisualPipeline;

            _resolver = resolver;
        }
        
        public void OnEvent(FightEvent evt)
        {
            evt.Defender.TryGetComponent(out Health defenderHealth);
            evt.Attacker.TryGetComponent(out Damage attackerDamage);
            
            defenderHealth.Value -= attackerDamage.Value;
            
            _fightVisualPipeline.AddTask(new AttackVisualTask(evt.Attacker, evt.Defender, _resolver));
            _fightVisualPipeline.AddTask(new ShowHeroViewTask(evt.Defender, _resolver));
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