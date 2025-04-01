using VContainer;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class TurnPipeline : Pipeline, IInitializable
    {
        private readonly IObjectResolver _objectResolver;
        
        public TurnPipeline(IObjectResolver resolver)
        {
            _objectResolver = resolver;
        }
        
        public void Initialize()
        {
            //ADD CALCULATION TASK
            AddTask(new StartVisualTurnPipelineTask(_objectResolver));
        }
    }
}