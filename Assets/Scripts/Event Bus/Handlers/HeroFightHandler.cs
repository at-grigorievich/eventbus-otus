using System;
using DefaultNamespace.Event_Bus.Events;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class HeroFightHandler: EventReceiver<FightEvent>, IInitializable, IDisposable
    {
        private readonly FightVisualPipeline _fightVisualPipeline;
        private readonly IObjectResolver _resolver;

        public HeroFightHandler(EventBus eventBus, FightVisualPipeline fightVisualPipeline, 
            IObjectResolver resolver): base(eventBus)
        {
            _fightVisualPipeline = fightVisualPipeline;
            _resolver = resolver;
        }
        
        public override void OnEvent(FightEvent evt)
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

        public void Initialize() => Enter();
        public void Dispose() => Exit();
    }
}