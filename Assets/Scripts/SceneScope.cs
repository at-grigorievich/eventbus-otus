using VContainer;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class SceneScope: LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<TurnPipeline>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<TurnPipelineRunner>();
        }
    }
}