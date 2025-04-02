using System;
using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public sealed class CreateHeroesHandler: EventReceiver<CreateHeroesEvent>, IInitializable, IDisposable
    {
        private readonly HeroTeamsService _heroTeamsService;
        
        private readonly IObjectResolver _resolver;
        private readonly TurnVisualPipeline _turnVisualPipeline;
        
        public CreateHeroesHandler(HeroTeamsService heroTeamsService, EventBus eventBus, 
            IObjectResolver resolver, TurnVisualPipeline turnVisualPipeline): base(eventBus)
        {
            _heroTeamsService = heroTeamsService;
            _resolver = resolver;
            _turnVisualPipeline = turnVisualPipeline;
        }
        
        public override void OnEvent(CreateHeroesEvent evt)
        {
            for (int i = 0; i < evt.HeroCount; i++)
            {
                Entity newHero = _heroTeamsService[evt.TeamType].AddNewHero();
                
                newHero.AddComponent(new ViewIndex { Value = i });
                _turnVisualPipeline.AddTask(new ShowHeroViewTask(newHero, _resolver));
            }
        }

        public void Initialize() => Enter();
        public void Dispose() => Exit();
    }
}