using DefaultNamespace.Event_Bus.Handlers;
using DefaultNamespace.Hero;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class SceneScope: LifetimeScope
    {
        [SerializeField] private LinearHeroFactoryCreator linearHeroCreator;
        [SerializeField] private UIService uiService;
        
        protected override void Configure(IContainerBuilder builder)
        {
            linearHeroCreator.Create(builder);

            builder.RegisterInstance(uiService).AsSelf();
            
            builder.Register<HeroTeamsService>(Lifetime.Singleton);
            builder.Register<EventBus>(Lifetime.Singleton);

            builder.Register<CreateHeroesHandler>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SelectMasterHeroHandler>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<HeroFightHandler>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<HeroDeathHandler>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<RechargeFightHandler>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<RootPipeline>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<TurnPipeline>(Lifetime.Singleton).AsSelf();
            builder.Register<TurnVisualPipeline>(Lifetime.Singleton).AsSelf();
            builder.Register<FightVisualPipeline>(Lifetime.Singleton).AsSelf();
        }
    }
}