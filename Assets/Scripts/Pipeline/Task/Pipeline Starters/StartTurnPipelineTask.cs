using VContainer;

namespace DefaultNamespace
{
    public class StartTurnPipelineTask: EventTask
    {
        private readonly TurnPipeline _turnPipeline;
        private readonly IObjectResolver _objectResolver;

        public StartTurnPipelineTask(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
            _turnPipeline = _objectResolver.Resolve<TurnPipeline>();
        }
        
        protected override void OnStart()
        {
            _turnPipeline.OnCompleted += OnPipelineCompleted;
            
            _turnPipeline.AddTask(new NextMoveTask(_objectResolver));
            _turnPipeline.AddTask(new StartVisualTurnPipelineTask(_objectResolver));
            _turnPipeline.AddTask(new SelectTargetByClickTask(_objectResolver));
            _turnPipeline.AddTask(new StartAttackVisualPipeline(_objectResolver));
            
            _turnPipeline.Reset();
            _turnPipeline.Run();
        }

        private void OnPipelineCompleted()
        {
            //_turnPipeline.OnCompleted -= OnPipelineCompleted;
            
            _turnPipeline.Reset();
            _turnPipeline.Run();
            //Complete();
        }
    }
}