using VContainer;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class CardFightPipeline: Pipeline, IInitializable
    {
        private const int UNITS_PER_TEAM = 4;
            
        private readonly IObjectResolver _objectResolver;
        
        public CardFightPipeline(IObjectResolver resolver)
        {
            _objectResolver = resolver;
        }
        
        public void Initialize()
        {
            AddTask(new CreateHeroesTask(_objectResolver, UNITS_PER_TEAM));
            AddTask(new StartTurnPipelineTask(_objectResolver));
            
            Reset();
            Run();
        }
    }
}