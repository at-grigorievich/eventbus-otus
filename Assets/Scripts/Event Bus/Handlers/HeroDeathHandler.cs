using System;
using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class HeroDeathHandler: IEventReceiver<DeathEvent>, IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
     
        private readonly HeroTeamsService _heroTeamsService;
        private readonly FightVisualPipeline _fightVisualPipeline;

        private readonly IObjectResolver _resolver;
        
        public UniqueId Id { get; } = new();

        public HeroDeathHandler(EventBus eventBus, HeroTeamsService heroTeamsService,
            FightVisualPipeline fightVisualPipeline, IObjectResolver resolver)
        {
            _eventBus = eventBus;
            _heroTeamsService = heroTeamsService;
            _fightVisualPipeline = fightVisualPipeline;
            
            _resolver = resolver;
        }
        
        public void OnEvent(DeathEvent evt)
        {
            TeamType deathTeam = (TeamType)evt.Entity.GetComponent<Team>().Value;
            _heroTeamsService[deathTeam].RemoveEntity(evt.Entity);
            
            _fightVisualPipeline.AddTask(new DeathHeroVisualTask(_resolver, evt.Entity));
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