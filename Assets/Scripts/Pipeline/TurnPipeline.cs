using VContainer;
using VContainer.Unity;

namespace DefaultNamespace
{
    public class TurnPipeline : Pipeline
    {
        private readonly IObjectResolver _objectResolver;
        
        public TurnPipeline(IObjectResolver resolver)
        {
            _objectResolver = resolver;
        }
    }
}