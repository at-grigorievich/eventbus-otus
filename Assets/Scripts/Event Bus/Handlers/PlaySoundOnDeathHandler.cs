using System;
using DefaultNamespace.Event_Bus.Events;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class PlaySoundOnDeathHandler: EventReceiver<PlaySoundOnDeathEvent>, IInitializable, IDisposable
    {
        private readonly TurnVisualPipeline _turnVisualPipeline;
        
        public PlaySoundOnDeathHandler(EventBus eventBus, TurnVisualPipeline turnVisualPipeline) : base(eventBus)
        {
            _turnVisualPipeline = turnVisualPipeline;
        }

        public override void OnEvent(PlaySoundOnDeathEvent evt)
        {
            if (evt.Entity.TryGetComponent(out DeathSound deathSound) == false) return;
            
            _turnVisualPipeline.AddTask(new PlaySoundTask(deathSound.Value, true));
        }

        public void Initialize() => Enter();
        public void Dispose() => Exit();
    }
}