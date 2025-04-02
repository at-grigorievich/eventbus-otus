using System;
using DefaultNamespace.Event_Bus.Events;
using DefaultNamespace.Hero;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace.Event_Bus.Handlers
{
    public class HeroDeathHandler: EventReceiver<DeathEvent>, IInitializable, IDisposable
    {
        private readonly HeroTeamsService _heroTeamsService;
        private readonly FightVisualPipeline _fightVisualPipeline;

        private readonly IObjectResolver _resolver;

        public HeroDeathHandler(EventBus eventBus, HeroTeamsService heroTeamsService,
            FightVisualPipeline fightVisualPipeline, IObjectResolver resolver): base(eventBus)
        {
            _heroTeamsService = heroTeamsService;
            _fightVisualPipeline = fightVisualPipeline;
            
            _resolver = resolver;
        }
        
        public override void OnEvent(DeathEvent evt)
        {
            TeamType deathTeam = (TeamType)evt.Entity.GetComponent<Team>().Value;
            _heroTeamsService[deathTeam].RemoveEntity(evt.Entity);
            
            _fightVisualPipeline.AddTask(new DeathHeroVisualTask(_resolver, evt.Entity));
        }

        public void Initialize() => Enter();

        public void Dispose() => Exit();
    }
}