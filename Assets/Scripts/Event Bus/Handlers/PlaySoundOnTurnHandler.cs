using System;
using DefaultNamespace.Event_Bus.Events;
using UnityEngine;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class PlaySoundOnTurnHandler: EventReceiver<PlayRandomTurnSoundEvent>, IInitializable, IDisposable
    {
        private readonly TurnVisualPipeline _turnVisualPipeline;
        
        public PlaySoundOnTurnHandler(EventBus eventBus, TurnVisualPipeline turnVisualPipeline) : base(eventBus)
        {
            _turnVisualPipeline = turnVisualPipeline;
        }

        public override void OnEvent(PlayRandomTurnSoundEvent evt)
        {
            if (evt.Entity.TryGetComponent(out StartTurnSounds startTurnSounds) == false) return;
            
            _turnVisualPipeline.AddTask(new PlaySoundTask(GetRandom(startTurnSounds.Values), false));
        }

        public void Initialize() => Enter();
        public void Dispose() => Exit();

        private AudioClip GetRandom(AudioClip[] clips)
        {
            int index = UnityEngine.Random.Range(0, clips.Length);

            return clips[index];
        }
    }
}