using DefaultNamespace.Event_Bus.Handlers;
using DefaultNamespace.Hero;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class SceneScope: LifetimeScope
    {
        [SerializeField] private LinearHeroFactoryCreator linearHeroCreator;
        
        protected override void Configure(IContainerBuilder builder)
        {
            linearHeroCreator.Create(builder);

            builder.Register<HeroTeamsService>(Lifetime.Singleton);
            builder.Register<EventBus>(Lifetime.Singleton);

            builder.Register<CreateHeroesHandler>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<CardFightPipeline>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}