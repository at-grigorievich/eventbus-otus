using System;
using DefaultNamespace.Event_Bus.Events;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class HeroFightHandler: IEventReceiver<FightEvent>, IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly FightVisualPipeline _fightVisualPipeline;
        private readonly IObjectResolver _resolver;
        
        public UniqueId Id { get; } = new();

        public HeroFightHandler(EventBus eventBus, FightVisualPipeline fightVisualPipeline, 
            IObjectResolver resolver)
        {
            _eventBus = eventBus;
            _fightVisualPipeline = fightVisualPipeline;

            _resolver = resolver;
        }
        
        public void OnEvent(FightEvent evt)
        {
            var defenderHealth = evt.Defender.GetComponent<Health>().Value;
            var attackerDamage = evt.Attacker.GetComponent<Damage>().Value;

            evt.Defender.GetComponent<Health>().Value = Mathf.Clamp(defenderHealth - attackerDamage, 0, int.MaxValue);
            
            _fightVisualPipeline.AddTask(new AttackVisualTask(evt.Attacker, evt.Defender, _resolver));
            _fightVisualPipeline.AddTask(new ShowHeroViewTask(evt.Defender, _resolver));
            
            if (evt.Defender.GetComponent<Health>().Value <= 0)
            {
                _eventBus.Raise(new DeathEvent(evt.Defender));
            }
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