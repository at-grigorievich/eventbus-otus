using VContainer;

namespace DefaultNamespace
{
    public class StartTurnPipelineTask: EventTask
    {
        private readonly TurnPipeline _turnPipeline;

        public StartTurnPipelineTask(IObjectResolver objectResolver)
        {
            _turnPipeline = objectResolver.Resolve<TurnPipeline>();
        }
        
        protected override void OnStart()
        {
            _turnPipeline.OnCompleted += OnPipelineCompleted;
            _turnPipeline.Reset();
            _turnPipeline.Run();
        }

        private void OnPipelineCompleted()
        {
            _turnPipeline.OnCompleted -= OnPipelineCompleted;
            Complete();
        }
    }
}